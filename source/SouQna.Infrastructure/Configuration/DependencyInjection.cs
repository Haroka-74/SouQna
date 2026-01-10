using Microsoft.EntityFrameworkCore;
using SouQna.Application.Interfaces;
using SouQna.Infrastructure.Persistence;
using Microsoft.Extensions.Configuration;
using SouQna.Infrastructure.Services.Email;
using Microsoft.Extensions.DependencyInjection;
using SouQna.Infrastructure.Services.Cryptography;
using SouQna.Infrastructure.Services.Authentication;
using SouQna.Infrastructure.Persistence.Repositories;

namespace SouQna.Infrastructure.Configuration
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<SouQnaDbContext>(options =>
            {
                options.UseNpgsql(configuration.GetConnectionString("DefaultConnection"));
            });

            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddScoped<IEmailService, EmailService>();
            services.AddScoped<ICryptoService, CryptoService>();
            services.AddScoped<IRandomTokenService, RandomTokenService>();
            services.AddScoped<IEmailTemplateService, EmailTemplateService>();

            return services;
        }
    }
}