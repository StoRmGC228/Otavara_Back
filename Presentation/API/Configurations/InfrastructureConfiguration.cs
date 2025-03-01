namespace API.Configurations
{
    using Infrastructure.Configurations;
    using Microsoft.EntityFrameworkCore;

    public static class InfrastructureConfiguration
    {
        public static IServiceCollection AddInfrastructureConfigurations(this IServiceCollection services,
            IConfiguration configuration)
        {
            var connectionString = configuration.GetValue<string>("ConnectionString");
            services.AddDbContext<OtavaraDbContext>(options => options.UseNpgsql(connectionString));
            return services;
        }
    }
}
