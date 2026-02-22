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
        private readonly Mock<IJwtService> _jwtServiceMock;
        private readonly Mock<IValidationService> _validationServiceMock;

        public AuthServiceTests()
        {
            _unitOfWorkMock = new Mock<IUnitOfWork>();
            _jwtServiceMock = new Mock<IJwtService>();
            _validationServiceMock = new Mock<IValidationService>();
            _authService = new AuthService(
                _unitOfWorkMock.Object,
                _jwtServiceMock.Object,
                _validationServiceMock.Object
            );
        }

        [Fact]
        public async Task RegisterAsync_ShouldReturnResponse_WhenDataIsUnique()
        {
            var request = new RegisterRequest("Samy", "Ali", "samy@test.com", "P@ssword12");

            _unitOfWorkMock.Setup(
                u => u.Users.AnyAsync(It.IsAny<Expression<Func<User, bool>>>())
            ).ReturnsAsync(false);

            var result = await _authService.RegisterAsync(request);

            result.Should().NotBeNull();
            result.FullName.Should().Be("Samy Ali");
            result.Email.Should().Be("samy@test.com");

            _validationServiceMock.Verify(v => v.ValidateAsync(request), Times.Once);
            _unitOfWorkMock.Verify(
                u => u.Users.AddAsync(
                    It.IsAny<User>()
                ),
                Times.Once
            );
            _unitOfWorkMock.Verify(
                u => u.SaveChangesAsync(),
                Times.Once
            );
        }

        [Fact]
        public async Task RegisterAsync_ShouldThrowConflictException_WhenEmailAlreadyExists()
        {
            var request = new RegisterRequest("Samy", "Ali", "samy@test.com", "P@ssword12");

            _unitOfWorkMock.Setup(
                u => u.Users.AnyAsync(It.IsAny<Expression<Func<User, bool>>>())
            ).ReturnsAsync(true);

            var action = async () => await _authService.RegisterAsync(request);

            await action.Should().ThrowAsync<ConflictException>();

            _validationServiceMock.Verify(v => v.ValidateAsync(request), Times.Once);
        }

        [Fact]
        public async Task LoginAsync_ShouldReturnToken_WhenCredentialsAreValid()
        {
            var password = "P@ssword12";
            var user = new User
            {
                Id = Guid.NewGuid(),
                FirstName = "samy",
                LastName = "Ali",
                Email = "samy@test.com",
                PasswordHash = BCrypt.Net.BCrypt.HashPassword(password),
                CreatedAt = DateTime.UtcNow
            };

            var request = new LoginRequest(user.Email, password);

            _unitOfWorkMock.Setup(
                u => u.Users.FindAsync(It.IsAny<Expression<Func<User, bool>>>())
            ).ReturnsAsync(user);

            _jwtServiceMock.Setup(
                jwt => jwt.Generate(It.IsAny<User>())
            ).Returns("jwt.test");

            var result = await _authService.LoginAsync(request);

            result.Should().NotBeNull();
            result.AccessToken.Should().Be("jwt.test");

            _validationServiceMock.Verify(v => v.ValidateAsync(request), Times.Once);
        }

        [Fact]
        public async Task LoginAsync_ShouldThrowUnauthorizedException_WhenUserDoesNotExist()
        {
            var request = new LoginRequest("samy@test.com", "P@ssword12");

            _unitOfWorkMock.Setup(
                u => u.Users.FindAsync(It.IsAny<Expression<Func<User, bool>>>())
            ).ReturnsAsync((User) null!);

            var action = async () => await _authService.LoginAsync(request);

            await action.Should().ThrowAsync<UnauthorizedException>();

            _validationServiceMock.Verify(v => v.ValidateAsync(request), Times.Once);
        }

        [Fact]
        public async Task LoginAsync_ShouldThrowUnauthorizedException_WhenPasswordIsIncorrect()
        {
            var user = new User
            {
                Id = Guid.NewGuid(),
                FirstName = "samy",
                LastName = "Ali",
                Email = "samy@test.com",
                PasswordHash = BCrypt.Net.BCrypt.HashPassword("P@ssword12"),
                CreatedAt = DateTime.UtcNow
            };

            var request = new LoginRequest(user.Email, "New-P@ssword12");

            _unitOfWorkMock.Setup(
                u => u.Users.FindAsync(It.IsAny<Expression<Func<User, bool>>>())
            ).ReturnsAsync(user);

            var action = async () => await _authService.LoginAsync(request);

            await action.Should().ThrowAsync<UnauthorizedException>();

            _validationServiceMock.Verify(v => v.ValidateAsync(request), Times.Once);
        }
    }
}