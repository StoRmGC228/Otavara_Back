namespace API.Configurations
{
    using Infrastructure.Configurations;
    using Infrastructure.Repositories;
    using Application.Interfaces;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.Extensions.DependencyInjection;

    public static class InfrastructureConfiguration
    {
        public static IServiceCollection AddInfrastructureConfigurations(this IServiceCollection services,
            IConfiguration configuration)
        {
            var connectionString = configuration.GetValue<string>("ConnectionString");
            services.AddDbContext<OtavaraDbContext>(options => options.UseNpgsql(connectionString));
            services.AddScoped(typeof(IBaseRepository<>), typeof(BaseRepository<>));
            return services;
        }
    }
}
