using System.Text;
using Microsoft.EntityFrameworkCore;
using SouQna.Application.Interfaces;
using Microsoft.IdentityModel.Tokens;
using SouQna.Infrastructure.Persistence;
using Microsoft.Extensions.Configuration;
using SouQna.Infrastructure.Services.Email;
using Microsoft.Extensions.DependencyInjection;
using SouQna.Infrastructure.Services.Cryptography;
using SouQna.Infrastructure.Configuration.Settings;
using SouQna.Infrastructure.Services.Authentication;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using SouQna.Infrastructure.Persistence.Repositories;
using SouQna.Infrastructure.Services.Files;

namespace SouQna.Infrastructure.Configuration
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            var jwtSettings = new JwtSettings();
            configuration.GetSection("JWT").Bind(jwtSettings);
            services.AddSingleton(jwtSettings);

            var emailSettings = new EmailSettings();
            configuration.GetSection("Email").Bind(emailSettings);
            services.AddSingleton(emailSettings);

            services.AddDbContextPool<SouQnaDbContext>(options =>
            {
                options.UseNpgsql(configuration.GetConnectionString("DefaultConnection"));
            });

            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(options =>
            {
                options.SaveToken = true;
                options.RequireHttpsMetadata = false;
                options.TokenValidationParameters = new TokenValidationParameters()
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateIssuerSigningKey = true,
                    ValidateLifetime = true,
                    ValidIssuer = jwtSettings.Issuer,
                    ValidAudience = jwtSettings.Audience,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtSettings.SecretKey)),
                    ClockSkew = TimeSpan.Zero
                };
            });

            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddScoped<IEmailService, EmailService>();
            services.AddScoped<ICryptoService, CryptoService>();
            services.AddScoped<IFileService, LocalFileService>();
            services.AddScoped<IJwtTokenService,JwtTokenService>();
            services.AddScoped<IRandomTokenService, RandomTokenService>();
            services.AddScoped<IEmailTemplateService, EmailTemplateService>();

            return services;
        }
    }
}