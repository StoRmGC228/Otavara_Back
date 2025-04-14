using Domain.Entities;

namespace Application.Interfaces
{
    public interface IRequestedCardRepository : IBaseRepository<Card>
    {
        Task<Card> GetByCodeAsync(string code);
    }
}
