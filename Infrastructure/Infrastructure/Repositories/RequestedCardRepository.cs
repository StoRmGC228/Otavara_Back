namespace Infrastructure.Repositories;

using Application.Interfaces;
using Configurations;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;

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