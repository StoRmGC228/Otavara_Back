namespace Application.Interfaces;

using System.Threading;
using Domain.DtoEntities;
using Domain.Entities;

public interface IBaseRepository<T> where T : IBaseEntity
{
    Task<IEnumerable<T>> GetAllAsync();
    Task<T?> GetByIdAsync(Guid id);
    Task<PaginatedDto<T>> GetPaginatedAsync(int pageSize, int pageNumber);
    Task<T> AddAsync(T entity);
    Task<T> UpdateAsync(Guid id, T entity);
    Task DeleteAsync(Guid id);
    void Update(T entity);
    Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
}