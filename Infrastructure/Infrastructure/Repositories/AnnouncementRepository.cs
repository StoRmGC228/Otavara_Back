namespace Infrastructure.Repositories;

using Application.Interfaces;
using Configurations;
using Domain.DtoEntities;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;

public class AnnouncementRepository : BaseRepository<Announcement>, IAnnouncementRepository
{
    private readonly DbSet<Announcement> _requestedCardDb;

    public AnnouncementRepository(OtavaraDbContext context) : base(context)
    {
        _requestedCardDb = context.Set<Announcement>();
    }

    public async Task<IEnumerable<Announcement>> GetByRequesterIdAsync(Guid requesterId)
    {
        return await _requestedCardDb.Where(a => a.RequesterId == requesterId).ToListAsync();
    }

    public async Task DeleteOverdueAnnouncements()
    {
        var twoWeeksAgo = DateOnly.FromDateTime(DateTime.UtcNow.AddDays(-14));

        await _requestedCardDb
            .Where(a => a.RequestedDate <= twoWeeksAgo)
            .ExecuteDeleteAsync();
    }

    public async Task<PaginatedDto<Announcement>> GetPaginatedAsync(int pageSize, int pageNumber)
    {
        var query = _requestedCardDb.AsQueryable().OrderByDescending(e => e.RequestedDate);


        var totalItems = await query.CountAsync();
        var items = await query.Skip((pageNumber - 1) * pageSize).Take(pageSize).ToListAsync();

        return new PaginatedDto<Announcement>
        {
            PaginatedEntities = items,
            TotalPages = (int)Math.Ceiling((double)totalItems / pageSize)
        };
    }
}