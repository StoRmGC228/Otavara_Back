using System.Reflection;
using Infrastructure.Configurations;
using Infrastructure.Seed;

namespace API.Configurations
{
    public static class DataSeederExtensions
    {
        public static IServiceCollection AddDataSeeders(this IServiceCollection services)
        {
            var seeders = Assembly.GetExecutingAssembly()
                .GetTypes()
                .Where(t => !t.IsAbstract && !t.IsInterface && typeof(IDataSeeder).IsAssignableFrom(t));

            foreach (var seeder in seeders)
            {
                services.AddScoped(typeof(IDataSeeder), seeder);
            }

            services.AddScoped<IDataSeederOrchestrator, DataSeederOrchestrator>();

            return services;
        }

        public static async Task<IApplicationBuilder> SeedDatabaseAsync(this IApplicationBuilder app)
        {
            using (var scope = app.ApplicationServices.CreateScope())
            {
                var services = scope.ServiceProvider;
                var dbContext = services.GetRequiredService<OtavaraDbContext>();

                // await dbContext.Database.MigrateAsync();

                if (!await dbContext.Database.CanConnectAsync())
                {
                    await dbContext.Database.EnsureCreatedAsync();
                }
                var seeder = services.GetRequiredService<IDataSeederOrchestrator>();
                await seeder.SeedAllAsync();
            }

            return app;
        }
    }
}
