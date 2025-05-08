namespace API.Configurations;

using Application.Interfaces;
using Infrastructure.Configurations;
using Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;
using Npgsql;

public static class InfrastructureConfiguration
{
    public static IServiceCollection AddInfrastructureConfigurations(this IServiceCollection services,
        IConfiguration configuration)
    {
        //var connectionString = configuration.GetValue<string>("ConnectionString");
        //var connectionString = Environment.GetEnvironmentVariable("DB_CONNECTION_STRING");
        var rawConnectionString = Environment.GetEnvironmentVariable("DB_CONNECTION_STRING")
                                  ?? Environment.GetEnvironmentVariable("DATABASE_URL");

        if (string.IsNullOrEmpty(rawConnectionString))
        {
            throw new InvalidOperationException("Database connection string is not set in environment variables.");
        }

        var connectionString = rawConnectionString;

        // Якщо строка у форматі URI (як на Render), перетвори її
        if (connectionString.StartsWith("postgresql://"))
        {
            var uri = new Uri(connectionString);
            var userInfo = uri.UserInfo.Split(':');

            var builder = new NpgsqlConnectionStringBuilder
            {
                Host = uri.Host,
                Port = uri.Port == -1 ? 5432 : uri.Port, // якщо порт не вказано, використовується стандартний 5432
                Username = userInfo[0],
                Password = userInfo[1],
                Database = uri.AbsolutePath.TrimStart('/'),
                SslMode = SslMode.Require,
                TrustServerCertificate = true
            };

            connectionString = builder.ConnectionString;
        }

        services.AddDbContext<OtavaraDbContext>(options => options.UseNpgsql(connectionString));
        services.AddScoped(typeof(IBaseRepository<>), typeof(BaseRepository<>));
        services.AddScoped<IUserRepository, UserRepository>();
        services.AddScoped<IEventRepository, EventRepository>();
        services.AddScoped<IParticipantsRepository, ParticipantsRepository>();
        services.AddScoped<IAnnouncementRepository, AnnouncementRepository>();
        services.AddScoped<IRequestedCardRepository, RequestedCardRepository>();
        services.AddScoped<IBookingRepository, BookingRepository>();
        services.AddScoped<IGoodRepository, GoodRepository>();
        return services;
    }
}