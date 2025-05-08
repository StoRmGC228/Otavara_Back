namespace Infrastructure.Repositories;

using Application.Interfaces;
using Configurations;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;

public class GoodRepository : BaseRepository<Good>, IGoodRepository
{
    private readonly OtavaraDbContext _context;
    private readonly DbSet<Good> _goods;

    public GoodRepository(OtavaraDbContext context) : base(context)
    {
        _context = context;
        _goods = context.Goods;
    }

    public async Task<IEnumerable<Good>> GetAllSortedByNameAsync(bool ascending)
    {
        return ascending
            ? await _goods.OrderBy(x => x.Name).ToListAsync()
            : await _goods.OrderByDescending(x => x.Name).ToListAsync();
    }

    public async Task<IEnumerable<Good>> GetAllSortedByQuantityAsync(bool ascending)
    {
        return ascending
            ? await _goods.OrderBy(x => x.QuantityInStock).ToListAsync()
            : await _goods.OrderByDescending(x => x.QuantityInStock).ToListAsync();
    }

    public async Task<IEnumerable<Good>> GetAllSortedByTimeAsync(bool ascending)
    {
        return ascending
            ? await _goods.OrderBy(x => x.CreatedAt).ToListAsync()
            : await _goods.OrderByDescending(x => x.CreatedAt).ToListAsync();
    }
}