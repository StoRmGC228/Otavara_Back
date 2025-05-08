namespace Application.Services;

using Interfaces;

public class DatabaseSeedService
{
    private readonly IDataSeederOrchestrator _orchestrator;

    public DatabaseSeedService(IDataSeederOrchestrator orchestrator)
    {
        _orchestrator = orchestrator;
    }

    public async Task SeedDatabaseAsync()
    {
        await _orchestrator.SeedAllAsync();
    }
}