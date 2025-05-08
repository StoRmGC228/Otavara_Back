namespace Application.Interfaces;

using Domain.Entities;

public interface IRequestedCardRepository : IBaseRepository<Card>
{
    Task<Card> GetByCodeAsync(string code);
}