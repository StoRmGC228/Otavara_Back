namespace Application.Services;
using Application.Interfaces;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

public class BaseService<T> : IBaseService<T> where T : BaseEntity
{
    protected readonly IBaseRepository<T> _repository;

    public BaseService(IBaseRepository<T> repository)
    {
        _repository = repository ?? throw new ArgumentNullException(nameof(repository));
    }

    public virtual async Task<IEnumerable<T>> GetAllAsync()
    {
        return await _repository.GetAllAsync();
    }

    public virtual async Task<T?> GetByIdAsync(int id)
    {
        return await _repository.GetByIdAsync(id);
    }

    public virtual async Task<T> AddAsync(T entity)
    {
        if (entity == null)
            throw new ArgumentNullException(nameof(entity));

        return await _repository.AddAsync(entity);
    }

    public virtual async Task<T> UpdateAsync(T entity)
    {
        if (entity == null)
            throw new ArgumentNullException(nameof(entity));

        return await _repository.UpdateAsync(entity);
    }

    public virtual async Task DeleteAsync(int id)
    {
        await _repository.DeleteAsync(id);
    }

    public virtual async Task<bool> ExistsAsync(int id)
    {
        var entity = await _repository.GetByIdAsync(id);
        return entity != null;
    }
}