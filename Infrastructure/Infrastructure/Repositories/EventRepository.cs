namespace Infrastructure.Repositories;

using Application.Interfaces;
using Configurations;
using Domain.DtoEntities;
using Domain.Entities;
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
        var items = query.Skip((pageNumber - 1) * pageSize).Take(pageSize).ToList();

        return new PaginatedDto<Event>
        {
            PaginatedEntities = items,
            TotalPages = (int)Math.Ceiling((double)totalItems / pageSize)
        };
    }

    public async Task<List<Event>> GetEventsSortedByDateAsync(bool ascending = true)
    {
        if (ascending)
        {
            return await _eventDb.OrderBy(e => e.EventStartTime).ToListAsync();
        }

        return await _eventDb.OrderByDescending(e => e.EventStartTime).ToListAsync();
    }

    public async Task<List<Event>> GetEventsByDateAsync(DateTime date)
    {
        return await _eventDb
            .Where(e => e.EventStartTime.Date == date.Date)
            .ToListAsync();
    }

    public async Task<List<Event>> GetEventsByDateRangeAsync(DateTime startDate, DateTime endDate)
    {
        return await _eventDb
            .Where(e => e.EventStartTime.Date >= startDate.Date && e.EventStartTime.Date <= endDate.Date)
            .ToListAsync();
    }

    public async Task<List<Event>> GetEventsByPriceRangeAsync(int minPrice, int maxPrice)
    {
        return await _eventDb
            .Where(e => e.Price >= minPrice && e.Price <= maxPrice)
            .ToListAsync();
    }

    public async Task<List<Event>> GetEventsByPriceRangeAndDateRangeAsync(
        int minPrice, int maxPrice, DateTime startDate, DateTime endDate)
    {
        return await _eventDb
            .Where(e => e.Price >= minPrice && e.Price <= maxPrice &&
                        e.EventStartTime.Date >= startDate.Date && e.EventStartTime.Date <= endDate.Date)
            .ToListAsync();
    }


    public async Task<List<Event>>? GetEventsByNameAndDateRangeAsync(string name, DateTime? startDate,
        DateTime? endDate)
    {
        if (startDate == null && endDate == null)
        {
            var result = await _eventDb.Where(e => e.Name == name).ToListAsync();
            return result;
        }

        if (endDate == null)
        {
            var timeLimit = startDate.Value.AddHours(23).AddMinutes(59);
            return await _eventDb
                .Where(e => e.Name == name && e.EventStartTime > startDate && e.EventStartTime < timeLimit)
                .ToListAsync();
        }


        return await _eventDb.Where(e => e.Name == name && e.EventStartTime >= startDate && e.EventStartTime <= endDate)
            .ToListAsync();
    }
}