namespace API.Configurations;

using System.Text.Json.Serialization;
using Cloudinary;
using Converters;
using Hangfire;
using Hangfire.PostgreSql;
using Npgsql;

public static class ApiConfigurations
{
    public static IServiceCollection AddApiConfigurations(this IServiceCollection services,
        IConfiguration configurations)
    {
        var rawConnectionString = configurations.GetValue<string>("DB_CONNECTION_STRING");

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

        AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);
        AppContext.SetSwitch("Npgsql.DisableDateTimeInfinityConversions", true);
        services.AddControllers().AddJsonOptions(options =>
        {
            options.JsonSerializerOptions.Converters.Add(new DateTimeConverter());
            options.JsonSerializerOptions.Converters.Add(new TimeOnlyConverter());
            options.JsonSerializerOptions.Converters.Add(new DateOnlyConverter());
            options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles;
        });
        services.AddScoped<IImageUploader, CloudinaryUploader>();
        services.AddSingleton<CloudinaryUploader>();
        services.AddHangfire(h => h.UsePostgreSqlStorage(connectionString));
        services.AddHangfireServer();

        return services;
    }
}