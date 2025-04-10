using Application.Interfaces;
using Domain.Entities;
using Infrastructure.Configurations;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories
{
    public class RequestedCardRepository : BaseRepository<RequestedCard>, IRequestedCardRepository
    {
        private readonly DbSet<RequestedCard> _requestedCardDb;
        public RequestedCardRepository(OtavaraDbContext context) : base(context)
        {
            _requestedCardDb = context.Set<RequestedCard>();
        }
        public async Task<IEnumerable<RequestedCard>> GetByRequesterIdAsync(Guid requesterId)
        {
            return await _requestedCardDb.Where(rc => rc.RequesterId == requesterId).ToListAsync();
        }
        public async Task<IEnumerable<RequestedCard>> GetByEventIdAsync(Guid eventId)
        {
            return await _requestedCardDb.Where(rc => rc.EventId == eventId).ToListAsync();
        }
        public async Task<RequestedCard?> GetByCodeAsync(string code)
        {
            return await _requestedCardDb.FirstOrDefaultAsync(rc => rc.Code == code);
        }
        public async Task<RequestedCard?> GetByLinkAsync(string link)
        {
            return await _requestedCardDb.FirstOrDefaultAsync(rc => rc.Link == link);
        }
    }
}
