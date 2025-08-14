namespace Infrastructure.Repositories;

using Application.Interfaces;
using Configurations;
using Domain.DtoEntities;
using Domain.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;

public class EventRepository : BaseRepository<Event>, IEventRepository
{
    private readonly OtavaraDbContext _context;
    private readonly DbSet<Event> _eventDb;

    public EventRepository(OtavaraDbContext context) : base(context)
    {
        _context = context;
        _eventDb = context.Set<Event>();
    }

    public async Task<PaginatedDto<Event>> GetPaginatedAsync(int pageSize, int pageNumber)
    {
        var query = _eventDb.AsQueryable().OrderByDescending(e => e.EventStartTime);


        var totalItems = await query.CountAsync();
        var items = await query.Skip((pageNumber - 1) * pageSize).Take(pageSize).ToListAsync();

        return new PaginatedDto<Event>
        {
            PaginatedEntities = items,
            TotalPages = (int)Math.Ceiling((double)totalItems / pageSize)
        };
    }

    [Authorize]
    public async Task<List<Event>> GetEventsByDateRangeAsync(DateTime? startDate, DateTime? endDate)
    {
        IQueryable<Event> query = _eventDb;
        if (startDate != null && endDate == null)
        {
            var timeLimit = startDate.Value.Date.AddHours(23).AddMinutes(59);
            query = query.Where(e => e.EventStartTime >= startDate && e.EventStartTime <= timeLimit)
                .OrderByDescending(e => e.EventStartTime);
        }
        else if (startDate != null && endDate != null)
        {
            query = query.Where(e => e.EventStartTime >= startDate && e.EventStartTime <= endDate)
                .OrderByDescending(e => e.EventStartTime);
        }

        return await query.ToListAsync();
    }

    [Authorize]
    public async Task<List<Event>> GetEventsByPriceRangeAsync(int minPrice, int maxPrice)
    {
        return await _eventDb
            .Where(e => e.Price >= minPrice && e.Price <= maxPrice)
            .ToListAsync();
    }

    [Authorize]
    public async Task<List<Event>> GetEventsByPriceRangeAndDateRangeAsync(
        int minPrice, int maxPrice, DateTime startDate, DateTime endDate)
    {
        return await _eventDb
            .Where(e => e.Price >= minPrice && e.Price <= maxPrice &&
                        e.EventStartTime.Date >= startDate.Date && e.EventStartTime.Date <= endDate.Date)
            .ToListAsync();
    }


    [Authorize]
    public async Task<List<Event>> GetEventsByNameAndDateRangeAsync(string name, DateTime? startDate, DateTime? endDate)
    {
        IQueryable<Event> query = _eventDb;

        if (!string.IsNullOrWhiteSpace(name))
        {
            query = query.Where(e => e.Name.ToLower().Contains(name.ToLower()));
        }

        if (startDate != null && endDate == null)
        {
            var timeLimit = startDate.Value.Date.AddHours(23).AddMinutes(59);
            query = query.Where(e => e.EventStartTime >= startDate && e.EventStartTime <= timeLimit);
        }
        else if (startDate != null && endDate != null)
        {
            query = query.Where(e => e.EventStartTime >= startDate && e.EventStartTime <= endDate);
        }

        return await query.ToListAsync();
    }
}