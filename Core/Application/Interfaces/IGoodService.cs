using Domain.Entities;

namespace Application.Interfaces
{
    public interface IGoodService
    {
        Task<IEnumerable<Good>> GetAllSortedByNameAsync(bool ascending);
        Task<IEnumerable<Good>> GetAllSortedByQuantityAsync(bool ascending);
        Task<IEnumerable<Good>> GetAllSortedByTimeAsync(bool ascending);

    }
}
