namespace Application.Interfaces;

using Domain.Entities;

public interface IGoodRepository : IBaseRepository<Good>
{
    Task<IEnumerable<Good>> GetAllSortedByNameAsync(bool ascending);
    Task<IEnumerable<Good>> GetAllSortedByQuantityAsync(bool ascending);
    Task<IEnumerable<Good>> GetAllSortedByTimeAsync(bool ascending);
    Task<bool> DecreaseGoodQuantityAsync(Guid goodId, int count);
}