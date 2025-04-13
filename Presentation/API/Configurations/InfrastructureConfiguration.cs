namespace API.Configurations;

using Application.Interfaces;
using Infrastructure.Configurations;
using Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;

public static class InfrastructureConfiguration
{
    public static IServiceCollection AddInfrastructureConfigurations(this IServiceCollection services,
        IConfiguration configuration)
    {
        var connectionString = configuration.GetValue<string>("ConnectionString");
        services.AddDbContext<OtavaraDbContext>(options => options.UseNpgsql(connectionString));
        services.AddScoped(typeof(IBaseRepository<>), typeof(BaseRepository<>));
        services.AddScoped<IUserRepository, UserRepository>();
        services.AddScoped<IEventRepository, EventRepository>();
        services.AddScoped<IParticipantsRepository, ParticipantsRepository>();
        services.AddScoped<IBookingRepository, BookingRepository>();
        services.AddAuthorization(options =>
        {
            options.AddPolicy("AdminOnly", policy => policy.RequireRole("Admin"));
            options.AddPolicy("ModeratorAndAbove", policy => policy.RequireRole("Admin", "Moderator"));
            options.AddPolicy("UserPolicy", policy => policy.RequireRole("Admin", "Moderator", "User"));
        });
        return services;
    }
}