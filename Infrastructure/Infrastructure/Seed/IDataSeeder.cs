using Infrastructure.Configurations;

namespace Infrastructure.Seed
{
    public interface IDataSeeder
    {
        int Priority { get; }
        Task SeedAsync(OtavaraDbContext dbContext);
        Task<bool> HasDataAsync(OtavaraDbContext dbContext);
    }

    public interface IDataSeederOrchestrator
    {
        Task SeedAllAsync();
    }
}