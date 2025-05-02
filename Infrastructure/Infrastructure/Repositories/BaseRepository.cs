namespace Infrastructure.Repositories;

using Application.Interfaces;
using Domain.DtoEntities;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;

public class BaseRepository<T> : IBaseRepository<T> where T : class, IBaseEntity
{
    private readonly DbContext _context;
    private readonly DbSet<T> _dbSet;

    public BaseRepository(DbContext context)
    {
        _context = context;
        _dbSet = context.Set<T>();
    }

    public async Task<PaginatedDto<T>> GetPaginatedAsync(int pageSize, int pageNumber)
    {
        var query = _dbSet.AsQueryable().OrderBy(e=>e.Id);


        var totalItems = await query.CountAsync();
        var items = query.Skip((pageNumber - 1) * pageSize).Take(pageSize).ToList();

        return new PaginatedDto<T>
        {
            PaginatedEntities = items,
            TotalPages = (int)Math.Ceiling((double)totalItems / pageSize)
        };
    }

    public async Task<IEnumerable<T>> GetAllAsync()
    {
        return await _dbSet.ToListAsync();
    }

    public async Task<T?> GetByIdAsync(Guid id)
    {
        return await _dbSet.FindAsync(id);
    }

    public async Task<T> AddAsync(T entity)
    {
        var result = await _dbSet.AddAsync(entity);
        await _context.SaveChangesAsync();
        return result.Entity;
    }

    public async Task<T> UpdateAsync(Guid id,T entity)
    {
        var existingEntity = await _context.Set<T>().FindAsync(id);

        if (existingEntity == null)
        {
            throw new InvalidOperationException($"Entity of type {typeof(T).Name} with ID {id} not found.");
        }

        var responseEntity = _context.Update(entity);
        return (entity);
    }

    public async Task DeleteAsync(Guid id)
    {
        var entity = await _dbSet.FindAsync(id);
        if (entity != null)
        {
            _dbSet.Remove(entity);
            await _context.SaveChangesAsync();
        }
    }
}