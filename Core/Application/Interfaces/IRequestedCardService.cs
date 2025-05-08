namespace Application.Interfaces;

using Domain.Entities;

public interface IRequestedCardService : IBaseService<Card>
{
    Task<Card> GetByCodeAsync(string code);
    Task<bool> IsRequestedCardExistsAsync(string code);
}