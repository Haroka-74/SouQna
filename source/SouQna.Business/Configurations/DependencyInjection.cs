using FluentValidation;
using System.Reflection;
using SouQna.Business.Services;
using SouQna.Business.Interfaces;
using Microsoft.Extensions.DependencyInjection;

namespace SouQna.Business.Configurations
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddBusiness(
            this IServiceCollection services
        )
        {
            services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());

            services.AddScoped<IValidationService, ValidationService>();
            services.AddScoped<IAuthService, AuthService>();

            return services;
        }
    }
}