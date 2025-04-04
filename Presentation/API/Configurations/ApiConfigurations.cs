namespace API.Configurations;

using Converters;
using Microsoft.AspNetCore.Http.Json;

public static class ApiConfigurations
{
    public static IServiceCollection AddApiConfigurations(this IServiceCollection services,
        IConfiguration configurations)
    {
        services.Configure<JsonOptions>(opt =>
        {
            opt.SerializerOptions.Converters.Add(new DateTimeConverter());
            opt.SerializerOptions.Converters.Add(new TimeOnlyConverter());
            opt.SerializerOptions.Converters.Add(new DateOnlyConverter());
        });
        return services;
    }
}