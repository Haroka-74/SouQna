using SouQna.Presentation.Handlers;

namespace SouQna.Presentation.Configurations
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddExceptionHandlers(this IServiceCollection services)
        {
            services.AddExceptionHandler<ValidationExceptionHandler>();
            services.AddExceptionHandler<ConflictExceptionHandler>();
            services.AddExceptionHandler<GlobalExceptionHandler>();

            services.AddProblemDetails();
            return services;
        }
    }
}