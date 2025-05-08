namespace API.Configurations;

using System.Reflection;
using Application.Interfaces;
using Application.Services;
using Infrastructure.Configurations;
using Infrastructure.Seed;
using Infrastructure.Seed.Seeders;

public static class DataSeederExtensions
{
    public static IServiceCollection AddDataSeeders(this IServiceCollection services)
    {
        var seeders = Assembly.GetExecutingAssembly()
            .GetTypes()
            .Where(t => !t.IsAbstract && !t.IsInterface && typeof(IDataSeeder).IsAssignableFrom(t))
            .ToList();

        seeders.AddRange(
            Assembly.GetAssembly(typeof(UserSeeder))
                ?.GetTypes()
                .Where(t => !t.IsAbstract && !t.IsInterface && typeof(IDataSeeder).IsAssignableFrom(t))
            ?? Enumerable.Empty<Type>()
        );

        foreach (var seeder in seeders) services.AddScoped(typeof(IDataSeeder), seeder);

        services.AddScoped<IDataSeederOrchestrator, DataSeederOrchestrator>();
        services.AddScoped<DatabaseSeedService>();

        return services;
    }

    public static async Task<IApplicationBuilder> EnsureDatabaseCreatedAsync(this IApplicationBuilder app)
    {
        using (var scope = app.ApplicationServices.CreateScope())
        {
            var services = scope.ServiceProvider;
            var dbContext = services.GetRequiredService<OtavaraDbContext>();

            if (!await dbContext.Database.CanConnectAsync())
            {
                await dbContext.Database.EnsureCreatedAsync();
            }

            // await dbContext.Database.MigrateAsync();
        }

        return app;
    }
}