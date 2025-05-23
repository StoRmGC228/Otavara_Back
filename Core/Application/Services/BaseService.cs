namespace Application.Services;

using AutoMapper;
using Domain.DtoEntities;
using Domain.Entities;
using Interfaces;

public class BaseService<T> : IBaseService<T> where T : IBaseEntity
{
    private readonly IMapper _mapper;
    protected readonly IBaseRepository<T> _repository;

    public BaseService(IBaseRepository<T> repository, IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
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

    public async Task UpdateAsync(T entity)
    {
        var dbEntity = await _repository.GetByIdAsync(entity.Id);

        if (dbEntity == null)
        {
            return;
        }

        _mapper.Map(entity, dbEntity);

        await _repository.Update(dbEntity);
        await _repository.SaveChangesAsync();
    }

    public async Task UpdateAsync(T entity, Guid id)
    {
        var dbEntity = await _repository.GetByIdAsync(id);

        _mapper.Map(entity, dbEntity);

        _repository.Update(dbEntity);
    }

    public async Task DeleteAsync(Guid id)
    {
        await _repository.DeleteAsync(id);
    }
}