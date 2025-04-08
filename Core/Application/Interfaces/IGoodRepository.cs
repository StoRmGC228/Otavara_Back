using Domain.Entities;

namespace Application.Interfaces
{
    public interface IGoodRepository : IBaseRepository<Good>
    {
        Task<IEnumerable<Good>> GetAllSortedByNameAsync(bool ascending); // true - orderby, false - orderby descending
        Task<IEnumerable<Good>> GetAllSortedByQuantityAsync(bool ascending);// true - from largest to smallest quantity
        Task<IEnumerable<Good>> GetAllSortedByTimeAsync(bool ascending); // true - orderby(from smalest), false - orderby descending
    }
}
