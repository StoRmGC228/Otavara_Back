namespace Application.Interfaces;

public interface IDataSeeder
{
    int Priority { get; }
    Task SeedAsync();
    Task<bool> HasDataAsync();
}