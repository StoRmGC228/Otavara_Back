namespace API.Configurations;

using System.Text;
using API.BackgroundServices;
using Application.Interfaces;
using Application.Providers;
using Application.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;

public static class ApplicationConfiguration
{
    public static IServiceCollection AddApplicationConfigurations(this IServiceCollection services,
        IConfiguration configuration)
    {
        var jwtSecret = Environment.GetEnvironmentVariable("JWT_SECRET");
        var jwtIssuer = Environment.GetEnvironmentVariable("JWT_ISSUER");
        var jwtAudience = Environment.GetEnvironmentVariable("JWT_AUDIENCE");

        services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
            .AddJwtBearer(JwtBearerDefaults.AuthenticationScheme, options =>
            {
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    ValidIssuer = jwtIssuer,
                    ValidAudience = jwtAudience,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtSecret!))
                };
                options.Events = new JwtBearerEvents
                {
                    OnMessageReceived = context =>
                    {
                        if (context.Request.Cookies.ContainsKey("MySecretCookies"))
                        {
                            context.Token = context.Request.Cookies["MySecretCookies"];
                        }

                        return Task.CompletedTask;
                    }
                };
            });

        services.AddAuthorization();

        services.Configure<JwtOptions>(configuration.GetSection("JwtSettings"));
        services.AddScoped<IAuthService, AuthService>();
        services.AddScoped<IUserService, UserService>();
        services.AddScoped<IEventService, EventService>();
        services.AddScoped<IJwtProvider, JwtProvider>();
        services.AddScoped<IBookingService, BookingService>();
        services.AddHostedService<BookingExpirationService>();
        return services;
    }
}