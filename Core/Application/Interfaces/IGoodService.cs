namespace Application.Interfaces;

using Domain.Entities;

public interface IGoodService
{
    Task<IEnumerable<Good>> GetAllSortedByNameAsync(bool ascending);
    Task<IEnumerable<Good>> GetAllSortedByQuantityAsync(bool ascending);
    Task<IEnumerable<Good>> GetAllSortedByTimeAsync(bool ascending);
}