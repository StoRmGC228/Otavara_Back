namespace API.Configurations;

using System.Text.Json.Serialization;
using Cloudinary;
using Converters;

public static class ApiConfigurations
{
    public static IServiceCollection AddApiConfigurations(this IServiceCollection services,
        IConfiguration configurations)
    {
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

        return services;
    }
}