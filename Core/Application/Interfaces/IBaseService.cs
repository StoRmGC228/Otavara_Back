namespace Application.Interfaces;

using Domain.DtoEntities;
using Domain.Entities;

public interface IBaseService<T> where T : IBaseEntity
{
    Task<IEnumerable<T>> GetAllAsync();
    Task<T?> GetByIdAsync(Guid id);
    Task<PaginatedDto<T>> GetPaginateAsync(int pageSize, int pageNumber);
    Task<T> AddAsync(T entity);
    Task<T> UpdateAsync(T entity);
    Task DeleteAsync(Guid id);
}