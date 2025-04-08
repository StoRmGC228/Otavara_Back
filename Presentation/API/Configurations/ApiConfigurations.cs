namespace API.Configurations;

using System.Text.Json.Serialization;
using Converters;

public static class ApiConfigurations
{
    public static IServiceCollection AddApiConfigurations(this IServiceCollection services,
        IConfiguration configurations)
    {

        services.AddControllers().AddJsonOptions(options =>
        {
            options.JsonSerializerOptions.Converters.Add(new DateTimeConverter());
            options.JsonSerializerOptions.Converters.Add(new TimeOnlyConverter());
            options.JsonSerializerOptions.Converters.Add(new DateOnlyConverter());
            options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles;
        });

        return services;
    }
}