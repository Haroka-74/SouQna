<div style = "font-family: 'Roboto', sans-serif;">

# Architecture (souqna.v0)

- To view the codebase for the `souqna.v0` version, run: `git checkout souqna.v0`

```
├── 📁 source
│   ├── 📁 SouQna.Business
│   │   ├── 📁 Common
│   │   │   └── 📄 PagedResult.cs
│   │   ├── 📁 Configurations
│   │   │   └── 📄 DependencyInjection.cs
│   │   ├── 📁 Contracts
│   │   │   ├── 📁 Requests
│   │   │   │   ├── 📁 Authentication
│   │   │   │   │   ├── 📄 LoginRequest.cs
│   │   │   │   │   └── 📄 RegisterRequest.cs
│   │   │   │   ├── 📁 Carts
│   │   │   │   │   ├── 📄 AddToCartRequest.cs
│   │   │   │   │   └── 📄 UpdateCartItemRequest.cs
│   │   │   │   ├── 📁 Orders
│   │   │   │   │   ├── 📄 CreateOrderRequest.cs
│   │   │   │   │   └── 📄 GetOrdersRequest.cs
│   │   │   │   └── 📁 Products
│   │   │   │       ├── 📄 AddProductRequest.cs
│   │   │   │       ├── 📄 GetProductsRequest.cs
│   │   │   │       └── 📄 UpdateProductRequest.cs
│   │   │   ├── 📁 Responses
│   │   │   │   ├── 📁 Authentication
│   │   │   │   │   ├── 📄 LoginResponse.cs
│   │   │   │   │   └── 📄 RegisterResponse.cs
│   │   │   │   ├── 📁 Carts
│   │   │   │   │   ├── 📄 CartItemResponse.cs
│   │   │   │   │   └── 📄 CartResponse.cs
│   │   │   │   ├── 📁 Orders
│   │   │   │   │   ├── 📄 CreateOrderResponse.cs
│   │   │   │   │   ├── 📄 OrderDetailResponse.cs
│   │   │   │   │   ├── 📄 OrderItemResponse.cs
│   │   │   │   │   ├── 📄 OrderSummaryResponse.cs
│   │   │   │   │   └── 📄 ShippingInfoResponse.cs
│   │   │   │   └── 📁 Products
│   │   │   │       └── 📄 ProductResponse.cs
│   │   │   └── 📁 Validators
│   │   │       ├── 📁 Authentication
│   │   │       │   ├── 📄 LoginRequestValidator.cs
│   │   │       │   └── 📄 RegisterRequestValidator.cs
│   │   │       ├── 📁 Carts
│   │   │       │   ├── 📄 AddToCartRequestValidator.cs
│   │   │       │   └── 📄 UpdateCartItemRequestValidator.cs
│   │   │       ├── 📁 Orders
│   │   │       │   ├── 📄 CreateOrderRequestValidator.cs
│   │   │       │   └── 📄 GetOrdersRequestValidator.cs
│   │   │       └── 📁 Products
│   │   │           ├── 📄 AddProductRequestValidator.cs
│   │   │           ├── 📄 GetProductsRequestValidator.cs
│   │   │           └── 📄 UpdateProductRequestValidator.cs
│   │   ├── 📁 Exceptions
│   │   │   ├── 📄 ConflictException.cs
│   │   │   ├── 📄 InvalidOrderStateException.cs
│   │   │   ├── 📄 NotFoundException.cs
│   │   │   └── 📄 UnauthorizedException.cs
│   │   ├── 📁 Interfaces
│   │   │   ├── 📄 IAuthService.cs
│   │   │   ├── 📄 ICartService.cs
│   │   │   ├── 📄 IOrderService.cs
│   │   │   ├── 📄 IPaymentService.cs
│   │   │   ├── 📄 IProductService.cs
│   │   │   └── 📄 IValidationService.cs
│   │   ├── 📁 Services
│   │   │   ├── 📄 AuthService.cs
│   │   │   ├── 📄 CartService.cs
│   │   │   ├── 📄 OrderService.cs
│   │   │   ├── 📄 PaymobService.cs
│   │   │   ├── 📄 ProductService.cs
│   │   │   └── 📄 ValidationService.cs
│   │   └── 📄 SouQna.Business.csproj
│   ├── 📁 SouQna.Infrastructure
│   │   ├── 📁 Configurations
│   │   │   ├── 📁 Settings
│   │   │   │   ├── 📄 JwtSettings.cs
│   │   │   │   ├── 📄 PaymobSettings.cs
│   │   │   │   └── 📄 ServerSettings.cs
│   │   │   └── 📄 DependencyInjection.cs
│   │   ├── 📁 Entities
│   │   │   ├── 📄 Cart.cs
│   │   │   ├── 📄 CartItem.cs
│   │   │   ├── 📄 Order.cs
│   │   │   ├── 📄 OrderItem.cs
│   │   │   ├── 📄 Payment.cs
│   │   │   ├── 📄 Product.cs
│   │   │   └── 📄 User.cs
│   │   ├── 📁 Enums
│   │   │   ├── 📄 OrderStatus.cs
│   │   │   └── 📄 PaymentStatus.cs
│   │   ├── 📁 Interfaces
│   │   │   ├── 📄 IJwtService.cs
│   │   │   ├── 📄 IRepository.cs
│   │   │   └── 📄 IUnitOfWork.cs
│   │   ├── 📁 Persistence
│   │   │   ├── 📁 Configurations
│   │   │   │   ├── 📄 CartConfiguration.cs
│   │   │   │   ├── 📄 CartItemConfiguration.cs
│   │   │   │   ├── 📄 OrderConfiguration.cs
│   │   │   │   ├── 📄 OrderItemConfiguration.cs
│   │   │   │   ├── 📄 PaymentConfiguration.cs
│   │   │   │   ├── 📄 ProductConfiguration.cs
│   │   │   │   └── 📄 UserConfiguration.cs
│   │   │   ├── 📁 Repositories
│   │   │   │   ├── 📄 Repository.cs
│   │   │   │   └── 📄 UnitOfWork.cs
│   │   │   └── 📄 SouQnaDbContext.cs
│   │   ├── 📁 Services
│   │   │   └── 📄 JwtService.cs
│   │   └── 📄 SouQna.Infrastructure.csproj
│   ├── 📁 SouQna.Presentation
│   │   ├── 📁 Configurations
│   │   │   └── 📄 DependencyInjection.cs
│   │   ├── 📁 Controllers
│   │   │   ├── 📄 AuthController.cs
│   │   │   ├── 📄 CartsController.cs
│   │   │   ├── 📄 OrdersController.cs
│   │   │   ├── 📄 PaymentsController.cs
│   │   │   └── 📄 ProductsController.cs
│   │   ├── 📁 Extensions
│   │   │   └── 📄 ClaimsPrincipalExtensions.cs
│   │   ├── 📁 Handlers
│   │   │   ├── 📄 ConflictExceptionHandler.cs
│   │   │   ├── 📄 GlobalExceptionHandler.cs
│   │   │   ├── 📄 InvalidOrderStateExceptionHandler.cs
│   │   │   ├── 📄 NotFoundExceptionHandler.cs
│   │   │   ├── 📄 UnauthorizedExceptionHandler.cs
│   │   │   └── 📄 ValidationExceptionHandler.cs
│   │   ├── 📁 Properties
│   │   │   └── ⚙️ launchSettings.json
│   │   ├── 📄 Program.cs
│   │   ├── 📄 SouQna.Presentation.csproj
│   │   └── ⚙️ appsettings.Development.json
│   └── 📄 SouQna.sln
├── 📁 tests
│   ├── 📁 SouQna.Business.Tests
│   │   ├── 📁 Services
│   │   │   ├── 📄 AuthServiceTests.cs
│   │   │   ├── 📄 CartServiceTests.cs
│   │   │   ├── 📄 OrderServiceTests.cs
│   │   │   ├── 📄 PaymobServiceTests.cs
│   │   │   └── 📄 ProductServiceTests.cs
│   │   ├── 📄 SouQna.Business.Tests.csproj
│   │   └── 📄 SouQna.Business.Tests.sln
│   └── 📄 Tests.sln
├── ⚙️ .gitignore
└── 📝 README.md
```

</div>