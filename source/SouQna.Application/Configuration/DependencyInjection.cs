using FluentValidation;
using System.Reflection;
using SouQna.Application.Behaviors;
using Microsoft.Extensions.DependencyInjection;

namespace SouQna.Application.Configuration
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddApplication(this IServiceCollection services)
        {
            services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());

            services.AddMediatR(c =>
            {
                c.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly());
                c.AddOpenBehavior(typeof(ValidationBehavior<,>));
            });

            return services;
        }
    }
}