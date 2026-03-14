using System.Text;
using Microsoft.EntityFrameworkCore;
using SouQna.Application.Interfaces;
using Microsoft.IdentityModel.Tokens;
using SouQna.Infrastructure.Settings;
using SouQna.Infrastructure.Services;
using SouQna.Infrastructure.Persistence;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using SouQna.Infrastructure.Persistence.Repositories;

namespace SouQna.Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructure(
            this IServiceCollection services,
            IConfiguration configuration
        )
        {
            var jwtSettings = new JwtSettings();
            configuration.GetSection("JWT").Bind(jwtSettings);
            services.AddSingleton(jwtSettings);

            var paymobSettings = new PaymobSettings();
            configuration.GetSection("Paymob").Bind(paymobSettings);
            services.AddSingleton(paymobSettings);

            var serverSettings = new ServerSettings();
            configuration.GetSection("Server").Bind(serverSettings);
            services.AddSingleton(serverSettings);

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
            services.AddScoped<IHasher, Hasher>();
            services.AddScoped<IJwtService, JwtService>();

            return services;
        }
    }
}