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
        services.AddScoped<IAnnouncementRepository, AnnouncementRepository>();
        services.AddScoped<IRequestedCardRepository, RequestedCardRepository>();
        services.AddScoped<IBookingRepository, BookingRepository>();
       
        return services;
    }
}