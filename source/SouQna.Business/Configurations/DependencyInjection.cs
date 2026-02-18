using FluentValidation;
using System.Reflection;
using SouQna.Business.Services;
using SouQna.Business.Interfaces;
using Microsoft.Extensions.DependencyInjection;
using SouQna.Infrastructure.Configurations.Settings;

namespace SouQna.Business.Configurations
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddBusiness(
            this IServiceCollection services
        )
        {
            services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());

            services.AddScoped<IAuthService, AuthService>();
            services.AddScoped<ICartService, CartService>();
            services.AddScoped<IOrderService, OrderService>();
            services.AddScoped<IProductService, ProductService>();
            services.AddHttpClient<IPaymentService, PaymobService>((sp, client) =>
            {
                var settings = sp.GetRequiredService<PaymobSettings>();
                client.BaseAddress = new Uri(settings.BaseAddress);
            });
            services.AddScoped<IValidationService, ValidationService>();

            return services;
        }
    }
}