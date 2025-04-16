using Application.Interfaces;
using Domain.Entities;
using Infrastructure.Configurations;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories
{
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
}
