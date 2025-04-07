using Infrastructure.Configurations;
using Application.Interfaces;

namespace Infrastructure.Seed;

public class DataSeederOrchestrator : IDataSeederOrchestrator
{
    private readonly OtavaraDbContext _dbContext;
    private readonly IEnumerable<IDataSeeder> _seeders;

    public DataSeederOrchestrator(OtavaraDbContext dbContext, IEnumerable<IDataSeeder> seeders)
    {
        _dbContext = dbContext;
        _seeders = seeders;
    }

    public async Task SeedAllAsync()
    {
        var orderedSeeders = _seeders.OrderBy(s => s.Priority).ToList();

        foreach (var seeder in orderedSeeders)
        {
            if (await seeder.HasDataAsync(_dbContext))
            {
                continue;
            }

            await seeder.SeedAsync(_dbContext);
        }
    }
}