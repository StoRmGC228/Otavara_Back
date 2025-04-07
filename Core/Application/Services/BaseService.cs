namespace Application.Services;

using Domain.DtoEntities;
using Domain.Entities;
using Interfaces;

public class BaseService<T> : IBaseService<T> where T : IBaseEntity
{
    protected readonly IBaseRepository<T> _repository;

    public BaseService(IBaseRepository<T> repository)
    {
        _repository = repository;
    }

    public async Task<IEnumerable<T>> GetAllAsync()
    {
        return await _repository.GetAllAsync();
    }

    public async Task<PaginatedDto<T>> GetPaginateAsync(int pageSize, int pageNumber)
    {
        return await _repository.GetPaginatedAsync(pageSize, pageNumber);
    }

    public async Task<T?> GetByIdAsync(Guid id)
    {
        return await _repository.GetByIdAsync(id);
    }

    public async Task<T> AddAsync(T entity)
    {
        return await _repository.AddAsync(entity);
    }

    public async Task<T> UpdateAsync(T entity)
    {
        return await _repository.UpdateAsync(entity);
    }

    public async Task DeleteAsync(Guid id)
    {
        await _repository.DeleteAsync(id);
    }
}