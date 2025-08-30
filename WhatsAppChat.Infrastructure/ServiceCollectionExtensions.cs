using System.Text;
using System.Security.Cryptography;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using WhatsAppChat.Application.Common.Interfaces;
using WhatsAppChat.Application.Common.Models;
using WhatsAppChat.Domain.Entities;
using WhatsAppChat.Infrastructure.Data;
using WhatsAppChat.Infrastructure.Repositories;
using WhatsAppChat.Infrastructure.Services;

namespace WhatsAppChat.Infrastructure;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        services.Configure<JwtSettings>(configuration.GetSection("JwtSettings"));

        services.AddDbContext<ApplicationDbContext>(options =>
            options.UseSqlServer(configuration.GetConnectionString("DefaultConnection")));

        services.AddIdentity<AppUser, IdentityRole>()
            .AddEntityFrameworkStores<ApplicationDbContext>()
            .AddDefaultTokenProviders();

        services.AddScoped<IRefreshTokenRepository, RefreshTokenRepository>();
        services.AddScoped<IContactRepository, ContactRepository>();
        services.AddScoped<IBlockRepository, BlockRepository>();
        services.AddScoped<IConversationRepository, ConversationRepository>();
        services.AddScoped<IJwtTokenService, JwtTokenService>();
        services.AddScoped<IAvatarStorageService, AvatarStorageService>();

        var jwtSettings = configuration.GetSection("JwtSettings").Get<JwtSettings>()!;
        // Hash the configured key to ensure a 256 bit key for HS256 regardless of
        // the original length provided in configuration.
        var key = SHA256.HashData(Encoding.UTF8.GetBytes(jwtSettings.Key));

        services.AddAuthentication(options =>
        {
            options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
            options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
        }).AddJwtBearer(options =>
        {
            options.TokenValidationParameters = new TokenValidationParameters
            {
                ValidateIssuer = true,
                ValidateAudience = true,
                ValidateIssuerSigningKey = true,
                ValidIssuer = jwtSettings.Issuer,
                ValidAudience = jwtSettings.Audience,
                IssuerSigningKey = new SymmetricSecurityKey(key)
            };
        });

        return services;
    }
}
