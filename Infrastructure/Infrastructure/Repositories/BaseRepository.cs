namespace Infrastructure.Repositories;

using Application.Interfaces;
using Domain.DtoEntities;
using Domain.Entities;
using Infrastructure.Configurations;
using Microsoft.EntityFrameworkCore;

public class BaseRepository<T> : IBaseRepository<T> where T : class, IBaseEntity
{
    private readonly OtavaraDbContext _context;
    private readonly DbSet<T> _dbSet;

    public BaseRepository(OtavaraDbContext context)
    {
        _context = context;
        _dbSet = context.Set<T>();
    }

    public async Task<PaginatedDto<T>> GetPaginatedAsync(int pageSize, int pageNumber)
    {
        var query = _dbSet.AsQueryable();


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

    public async Task<T> UpdateAsync(T entity)
    {
        _context.Attach(entity);
        _context.Entry(entity).State = EntityState.Modified;
        await _context.SaveChangesAsync();
        return entity;
    }

    public void Update(T entity)
    {
        _context.Attach(entity);
        _context.Entry(entity).State = EntityState.Modified;
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

    public async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        return await _context.SaveChangesAsync(cancellationToken);
    }
}