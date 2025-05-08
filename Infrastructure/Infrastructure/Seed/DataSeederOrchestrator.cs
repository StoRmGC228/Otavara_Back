namespace Infrastructure.Seed;

using Application.Interfaces;

public class DataSeederOrchestrator : IDataSeederOrchestrator
{
    private readonly IEnumerable<IDataSeeder> _seeders;

    public DataSeederOrchestrator(IEnumerable<IDataSeeder> seeders)
    {
        _seeders = seeders;
    }

    public async Task SeedAllAsync()
    {
        var orderedSeeders = _seeders.OrderBy(s => s.Priority).ToList();

        foreach (var seeder in orderedSeeders)
        {
            if (await seeder.HasDataAsync())
            {
                continue;
            }

            await seeder.SeedAsync();
        }
    }
}