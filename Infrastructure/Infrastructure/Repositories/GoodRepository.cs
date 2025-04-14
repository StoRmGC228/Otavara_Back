namespace Infrastructure.Repositories;

using Application.Interfaces;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;

public class GoodRepository : BaseRepository<Good>, IGoodRepository
{
    private readonly DbContext _context;

    public GoodRepository(DbContext context) : base(context)
    {
        _context = context;
    }

    public async Task<IEnumerable<Good>> GetAllSortedByNameAsync(bool ascending)
    {
        var goodSet = _context.Set<Good>();
        return ascending
            ? await goodSet.OrderBy(x => x.Name).ToListAsync()
            : await goodSet.OrderByDescending(x => x.Name).ToListAsync();
    }

    public async Task<IEnumerable<Good>> GetAllSortedByQuantityAsync(bool ascending)
    {
        var goodSet = _context.Set<Good>();
        return ascending
            ? await goodSet.OrderBy(x => x.QuantityInStock).ToListAsync()
            : await goodSet.OrderByDescending(x => x.QuantityInStock).ToListAsync();
    }

    public async Task<IEnumerable<Good>> GetAllSortedByTimeAsync(bool ascending)
    {
        var goodSet = _context.Set<Good>();
        return ascending
            ? await goodSet.OrderBy(x => x.CreatedAt).ToListAsync()
            : await goodSet.OrderByDescending(x => x.CreatedAt).ToListAsync();
    }
}