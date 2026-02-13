using FluentValidation;
using System.Reflection;
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

            return services;
        }
    }
}