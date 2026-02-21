using Moq;
using FluentAssertions;
using System.Linq.Expressions;
using SouQna.Business.Services;
using SouQna.Business.Interfaces;
using SouQna.Business.Exceptions;
using SouQna.Infrastructure.Entities;
using SouQna.Infrastructure.Interfaces;
using SouQna.Business.Contracts.Requests.Authentication;

namespace SouQna.Business.Tests.Services
{
    public class AuthServiceTests
    {
        private readonly IAuthService _authService;
        private readonly Mock<IUnitOfWork> _unitOfWorkMock;
        private readonly Mock<IRepository<User>> _userRepositoryMock;
        private readonly Mock<IJwtService> _jwtServiceMock;
        private readonly Mock<IValidationService> _validationServiceMock;

        public AuthServiceTests()
        {
            _unitOfWorkMock = new Mock<IUnitOfWork>();
            _userRepositoryMock = new Mock<IRepository<User>>();
            _jwtServiceMock = new Mock<IJwtService>();
            _validationServiceMock = new Mock<IValidationService>();
            _unitOfWorkMock.Setup(u => u.Users).Returns(_userRepositoryMock.Object);
            _authService = new AuthService(
                _unitOfWorkMock.Object,
                _jwtServiceMock.Object,
                _validationServiceMock.Object
            );
        }

        [Fact]
        public async Task Register_WithValidRequest_ReturnsRegisterResponse()
        {
            var request = new RegisterRequest(
                "John",
                "Doe",
                "test@example.com",
                "P@ssword12"
            );

            _userRepositoryMock.Setup(
                r => r.AnyAsync(It.IsAny<Expression<Func<User, bool>>>())
            ).ReturnsAsync(false);

            var result = await _authService.RegisterAsync(request);

            result.Should().NotBeNull();
            result.FullName.Should().Be("John Doe");
            result.Email.Should().Be(request.Email);

            _validationServiceMock.Verify(v => v.ValidateAsync(request), Times.Once);
            _userRepositoryMock.Verify(r => r.AddAsync(It.Is<User>(u => u.Email == request.Email)), Times.Once);
            _unitOfWorkMock.Verify(u => u.SaveChangesAsync(), Times.Once);
        }

        [Fact]
        public async Task Register_WithExistingEmail_ThrowsConflictException()
        {
            var request = new RegisterRequest(
                "John",
                "Doe",
                "test@example.com",
                "P@ssword12"
            );

            _userRepositoryMock.Setup(
                r => r.AnyAsync(It.IsAny<Expression<Func<User, bool>>>())
            ).ReturnsAsync(true);

            var action = async () => await _authService.RegisterAsync(request);

            await action.Should().ThrowAsync<ConflictException>();
            _validationServiceMock.Verify(v => v.ValidateAsync(request), Times.Once);
        }

        [Fact]
        public async Task Login_WithValidCredentials_ReturnsLoginResponse()
        {
            var request = new LoginRequest(
                "test@example.com",
                "P@ssword12"
            );

            _userRepositoryMock.Setup(
                r => r.FindAsync(It.IsAny<Expression<Func<User, bool>>>())
            ).ReturnsAsync(new User
            {
                Id = Guid.NewGuid(),
                FirstName = "John",
                LastName = "Doe",
                Email = request.Email,
                PasswordHash = BCrypt.Net.BCrypt.HashPassword(request.Password),
                CreatedAt = DateTime.UtcNow
            });

            _jwtServiceMock.Setup(
                j => j.Generate(It.Is<User>(u => u.Email == request.Email))
            ).Returns("jwt.test");

            var result = await _authService.LoginAsync(request);

            result.Should().NotBeNull();
            result.AccessToken.Should().Be("jwt.test");

            _validationServiceMock.Verify(v => v.ValidateAsync(request), Times.Once);
        }

        [Fact]
        public async Task Login_WithNonExistentUser_ThrowsUnauthorizedException()
        {
            var request = new LoginRequest(
                "test@example.com",
                "P@ssword12"
            );

            _userRepositoryMock.Setup(
                r => r.FindAsync(It.IsAny<Expression<Func<User, bool>>>())
            ).ReturnsAsync((User) null!);

            var action = async () => await _authService.LoginAsync(request);

            await action.Should().ThrowAsync<UnauthorizedException>();

            _validationServiceMock.Verify(v => v.ValidateAsync(request), Times.Once);
        }

        [Fact]
        public async Task Login_WithWrongPassword_ThrowsUnauthorizedException()
        {
            var request = new LoginRequest(
                "test@example.com",
                "P@ssword12"
            );

            _userRepositoryMock.Setup(
                r => r.FindAsync(It.IsAny<Expression<Func<User, bool>>>())
            ).ReturnsAsync(new User
            {
                Id = Guid.NewGuid(),
                FirstName = "John",
                LastName = "Doe",
                Email = request.Email,
                PasswordHash = BCrypt.Net.BCrypt.HashPassword("NewP@ssword12"),
                CreatedAt = DateTime.UtcNow
            });

            var action = async () => await _authService.LoginAsync(request);

            await action.Should().ThrowAsync<UnauthorizedException>();

            _validationServiceMock.Verify(v => v.ValidateAsync(request), Times.Once);
        }
    }
}