using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace Moneyon.PowerBi.API.Common.JWT;

public static class JwtExtensions
{
    
    public static void AddJwtAuthentication(this IServiceCollection services, JwtSettings settings)
    {
        services.AddAuthentication(opt =>
        {
            opt.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
            opt.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
        })
        .AddJwtBearer(options =>
        {
            options.RequireHttpsMetadata = false;
            options.SaveToken = true;
            options.TokenValidationParameters = new Microsoft.IdentityModel.Tokens.TokenValidationParameters()
            {
                ValidateIssuerSigningKey = settings.ValidateIssuerSigningKey,
                IssuerSigningKey = new SymmetricSecurityKey(System.Text.Encoding.UTF8.GetBytes(settings.IssuerSigningKey)),
                ValidateIssuer = settings.ValidateIssuer,
                ValidIssuer = settings.ValidIssuer,
                ValidateAudience = settings.ValidateAudience,
                ValidAudience = settings.ValidAudience,
                RequireExpirationTime = settings.RequireExpirationTime,
                ValidateLifetime = settings.RequireExpirationTime,
                ClockSkew = TimeSpan.FromDays(1),
            };
        });
    }
}
