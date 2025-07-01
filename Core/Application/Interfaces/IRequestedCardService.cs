namespace Application.Interfaces;

using Domain.Entities;

public interface IRequestedCardService : IBaseService<Card>
{
    Task<bool> IsRequestedCardExistsAsync(Guid id);
}