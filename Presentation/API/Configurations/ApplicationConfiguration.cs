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
        services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
            .AddJwtBearer(JwtBearerDefaults.AuthenticationScheme, options =>
            {
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = false,
                    ValidateAudience = false,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(
                        Encoding.UTF8.GetBytes(configuration.GetSection("JwtSettings").Get<JwtOptions>().SecretKey))
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
        services.AddAuthorization(options =>
        {
            options.AddPolicy("AdminOnly", policy => policy.RequireRole("Admin"));
            options.AddPolicy("ModeratorAndAbove", policy => policy.RequireRole("Admin", "Moderator"));
            options.AddPolicy("UserPolicy", policy => policy.RequireRole("Admin", "Moderator", "User"));
        });
        services.Configure<JwtOptions>(configuration.GetSection("JwtSettings"));
        services.AddScoped<IAuthService, AuthService>();
        services.AddScoped<IUserService, UserService>();
        services.AddScoped<IRequestedCardService, RequestedCardService>();
        services.AddScoped<IAnnouncementService, AnnouncementService>();
        services.AddScoped<IEventService, EventService>();
        services.AddScoped<IJwtProvider, JwtProvider>();
        services.AddScoped<IBookingService, BookingService>();
        services.AddHostedService<BookingExpirationService>();
        return services;
    }
}