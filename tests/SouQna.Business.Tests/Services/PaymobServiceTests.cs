using Moq;
using System.Net;
using System.Text;
using FluentAssertions;
using System.Text.Json;
using System.Linq.Expressions;
using SouQna.Business.Services;
using SouQna.Business.Exceptions;
using SouQna.Infrastructure.Enums;
using SouQna.Infrastructure.Entities;
using SouQna.Infrastructure.Interfaces;
using SouQna.Infrastructure.Configurations.Settings;

namespace SouQna.Business.Tests.Services
{
    internal class HttpMessageHandlerMock(
        string response,
        HttpStatusCode statusCode
    ) : HttpMessageHandler
    {
        protected override Task<HttpResponseMessage> SendAsync(
            HttpRequestMessage request,
            CancellationToken cancellationToken
        )
        {
            return Task.FromResult(new HttpResponseMessage(statusCode)
            {
                Content = new StringContent(response, Encoding.UTF8, "application/json")
            });
        }
    }
    public class PaymobServiceTests
    {
        private readonly Mock<IUnitOfWork> _unitOfWorkMock;
        private readonly PaymobSettings _paymobSettings;
        private readonly ServerSettings _serverSettings;

        public PaymobServiceTests()
        {
            _unitOfWorkMock = new Mock<IUnitOfWork>();
            _paymobSettings = new PaymobSettings
            {
                BaseAddress = "base-address",
                SecretKey = "secret-key",
                PublicKey = "public-key",
                HmacSecret = "hmac-secret"
            };
            _serverSettings = new ServerSettings
            {
                BaseAddress = "base-address"
            };
        }

        private PaymobService CreateService(HttpClient client)
        {
            return new PaymobService(
                client,
                _unitOfWorkMock.Object,
                _paymobSettings,
                _serverSettings
            );
        }

        [Fact]
        public async Task CreatePaymentIntentAsync_ShouldThrowNotFoundException_WhenOrderDoesNotExist()
        {
            _unitOfWorkMock.Setup(
                u => u.Orders.FindAsync(
                    It.IsAny<Expression<Func<Order, bool>>>(),
                    It.IsAny<Expression<Func<Order, object>>[]>()
                )
            ).ReturnsAsync((Order) null!);

            var service = CreateService(new HttpClient());

            var action = async () => await service.CreatePaymentIntentAsync(Guid.NewGuid());

            await action.Should().ThrowAsync<NotFoundException>();
        }

        [Fact]
        public async Task CreatePaymentIntentAsync_ShouldThrowInvalidOrderStateException_WhenOrderIsNotPending()
        {
            var orderId = Guid.NewGuid();
            var order = new Order
            {
                Id = orderId,
                OrderNumber = "ORD-2026-0001",
                UserId = Guid.NewGuid(),
                Total = 2450.75m,
                Status = OrderStatus.Confirmed,
                ShippingPhoneNumber = "01012345678",
                CreatedAt = DateTime.UtcNow,
            };

            _unitOfWorkMock.Setup(
                u => u.Orders.FindAsync(
                    It.IsAny<Expression<Func<Order, bool>>>(),
                    It.IsAny<Expression<Func<Order, object>>[]>()
                )
            ).ReturnsAsync(order);

            var service = CreateService(new HttpClient());

            var action = async () => await service.CreatePaymentIntentAsync(orderId);

            await action.Should().ThrowAsync<InvalidOrderStateException>();
        }

        [Fact]
        public async Task CreatePaymentIntentAsync_ShouldThrowConflictException_WhenPendingPaymentAlreadyExists()
        {
            var orderId = Guid.NewGuid();
            var order = new Order
            {
                Id = orderId,
                OrderNumber = "ORD-2026-0001",
                UserId = Guid.NewGuid(),
                Total = 2450.75m,
                Status = OrderStatus.Pending,
                ShippingPhoneNumber = "01012345678",
                CreatedAt = DateTime.UtcNow,
            };
            var payment = new Payment
            {
                Id = Guid.NewGuid(),
                OrderId = orderId,
                Amount = order.Total,
                Status = PaymentStatus.Pending,
                TransactionId = null,
                IntentId = Guid.NewGuid().ToString(),
                CreatedAt = DateTime.UtcNow,
            };

            _unitOfWorkMock.Setup(
                u => u.Orders.FindAsync(
                    It.IsAny<Expression<Func<Order, bool>>>(),
                    It.IsAny<Expression<Func<Order, object>>[]>()
                )
            ).ReturnsAsync(order);

            _unitOfWorkMock.Setup(
                u => u.Payments.FindAsync(It.IsAny<Expression<Func<Payment, bool>>>())
            ).ReturnsAsync(payment);

            var service = CreateService(new HttpClient());

            var action = async () => await service.CreatePaymentIntentAsync(orderId);

            await action.Should().ThrowAsync<ConflictException>();
        }

        [Fact]
        public async Task CreatePaymentIntentAsync_ShouldThrowConflictException_WhenOrderIsAlreadyPaid()
        {
            var orderId = Guid.NewGuid();
            var order = new Order
            {
                Id = orderId,
                OrderNumber = "ORD-2026-0001",
                UserId = Guid.NewGuid(),
                Total = 2450.75m,
                Status = OrderStatus.Pending,
                ShippingPhoneNumber = "01012345678",
                CreatedAt = DateTime.UtcNow,
            };
            var payment = new Payment
            {
                Id = Guid.NewGuid(),
                OrderId = orderId,
                Amount = order.Total,
                Status = PaymentStatus.Succeeded,
                TransactionId = null,
                IntentId = Guid.NewGuid().ToString(),
                CreatedAt = DateTime.UtcNow,
            };

            _unitOfWorkMock.Setup(
                u => u.Orders.FindAsync(
                    It.IsAny<Expression<Func<Order, bool>>>(),
                    It.IsAny<Expression<Func<Order, object>>[]>()
                )
            ).ReturnsAsync(order);

            _unitOfWorkMock.Setup(
                u => u.Payments.FindAsync(It.IsAny<Expression<Func<Payment, bool>>>())
            ).ReturnsAsync(payment);

            var service = CreateService(new HttpClient());

            var action = async () => await service.CreatePaymentIntentAsync(orderId);

            await action.Should().ThrowAsync<ConflictException>();
        }

        [Fact]
        public async Task CreatePaymentIntentAsync_ShouldReturnCheckoutUrl_WhenOrderIsValidAndPaymobRespondsSuccessfully()
        {
            var orderId = Guid.NewGuid();
            var user = new User
            {
                Id = Guid.NewGuid(),
                FirstName = "samy",
                LastName = "Ali",
                Email = "samy@test.com",
                PasswordHash = BCrypt.Net.BCrypt.HashPassword("P@ssword12"),
                CreatedAt = DateTime.UtcNow
            };
            var order = new Order
            {
                Id = orderId,
                OrderNumber = "ORD-2026-0001",
                UserId = Guid.NewGuid(),
                Total = 2450.75m,
                Status = OrderStatus.Pending,
                ShippingFullName = "Mohamed Mahmoud",
                ShippingAddressLine = "15 Tahrir Street, Apt 3",
                ShippingCity = "Cairo",
                ShippingPhoneNumber = "01012345678",
                CreatedAt = DateTime.UtcNow,
                User = user
            };
            order.OrderItems.Add(new OrderItem
            {
                Id = Guid.NewGuid(),
                OrderId = orderId,
                ProductId = Guid.NewGuid(),
                ProductName = "HP Pavilion 14",
                ProductDescription = "14-inch laptop with Intel Core i5, 8GB RAM, and 512GB SSD.",
                ProductImage = "4AAQSkZJRgABAQEASABIAAD2w",
                Price = 1899.99m,
                Quantity = 1
            });

            _unitOfWorkMock.Setup(
                u => u.Orders.FindAsync(
                    It.IsAny<Expression<Func<Order, bool>>>(),
                    It.IsAny<Expression<Func<Order, object>>[]>()
                )
            ).ReturnsAsync(order);

            _unitOfWorkMock.Setup(
                u => u.Payments.FindAsync(It.IsAny<Expression<Func<Payment, bool>>>())
            ).ReturnsAsync((Payment) null!);

            var handler = new HttpMessageHandlerMock(
                JsonSerializer.Serialize(new
                {
                    id = "id",
                    client_secret = "client-secret",
                    redirection_url = "redirection_url"
                }),
                HttpStatusCode.OK
            );

            var service = CreateService(new HttpClient(handler)
            {
                BaseAddress = new Uri("https://accept.paymob.com")
            });

            var result = await service.CreatePaymentIntentAsync(orderId);

            result.Should().NotBeNullOrWhiteSpace();
            result.Should().Contain("publicKey=public-key");
            result.Should().Contain("clientSecret=client-secret");

            _unitOfWorkMock.Verify(
                u => u.Payments.AddAsync(It.IsAny<Payment>()),
                Times.Once
            );
            _unitOfWorkMock.Verify(u => u.SaveChangesAsync(), Times.Once);
        }

        [Fact]
        public async Task ProcessPaymentWebhookAsync_ShouldThrowUnauthorizedException_WhenHmacIsInvalid()
        {
            var service = CreateService(new HttpClient());

            var json = BuildWebhookJson(Guid.NewGuid(), success: true);

            var action = async () => await service.ProcessPaymentWebhookAsync(json, "invalid-hmac");

            await action.Should().ThrowAsync<UnauthorizedException>();
        }

        private static string BuildWebhookJson(Guid orderId, bool success)
        {
            var payload = new
            {
                obj = new
                {
                    amount_cents = 208900,
                    created_at = "2026-02-01T12:00:00",
                    currency = "EGP",
                    error_occured = false,
                    has_parent_transaction = false,
                    id = 987654321,
                    integration_id = 5514498,
                    is_3d_secure = true,
                    is_auth = false,
                    is_capture = false,
                    is_refunded = false,
                    is_standalone_payment = true,
                    is_voided = false,
                    order = new { id = 111222333 },
                    owner = 99887766,
                    pending = false,
                    source_data = new
                    {
                        pan = "2346",
                        sub_type = "MasterCard",
                        type = "card"
                    },
                    success = success,
                    payment_key_claims = new
                    {
                        extra = new
                        {
                            order_id = orderId.ToString()
                        }
                    }
                }
            };

            return JsonSerializer.Serialize(payload);
        }
    }
}