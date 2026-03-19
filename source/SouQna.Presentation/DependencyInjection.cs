using SouQna.Presentation.Handlers;

namespace SouQna.Presentation
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddExceptionHandlers(this IServiceCollection services)
        {
            services.AddExceptionHandler<ConflictExceptionHandler>();
            services.AddExceptionHandler<UnauthorizedExceptionHandler>();
            services.AddExceptionHandler<NotFoundExceptionHandler>();
            services.AddExceptionHandler<InsufficientStockExceptionHandler>();
            services.AddExceptionHandler<InvalidStateExceptionHandler>();
            services.AddExceptionHandler<GlobalExceptionHandler>();

            services.AddProblemDetails();
            return services;
        }
    }
}