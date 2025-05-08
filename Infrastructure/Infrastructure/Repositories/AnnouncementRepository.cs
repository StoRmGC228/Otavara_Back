namespace Infrastructure.Repositories;

using Application.Interfaces;
using Configurations;
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
}