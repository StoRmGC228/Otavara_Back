using Application.Interfaces;
using Domain.DtoEntities;
using Domain.Entities;
using Infrastructure.Configurations;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories
{
    public class RequestedCardRepository : BaseRepository<Card>, IRequestedCardRepository
    {
        private readonly DbSet<Card> _requestedCardDb;
        public RequestedCardRepository(OtavaraDbContext context) : base(context)
        {
            _requestedCardDb = context.Set<Card>();
        }
        public async Task<Card?> GetByCodeAsync(string code)
        {
            return await _requestedCardDb.FirstOrDefaultAsync(rc => rc.Code == code);
        }
    }
}
