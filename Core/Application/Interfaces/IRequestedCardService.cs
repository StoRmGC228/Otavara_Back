namespace Application.Interfaces;

using Domain.Entities;

public interface IRequestedCardService : IBaseService<RequestedCard>
{
    //Injected from IRecuestedCardRepository
    Task<IEnumerable<RequestedCard>> GetByRequesterIdAsync(Guid requesterId);
    Task<IEnumerable<RequestedCard>> GetByEventIdAsync(Guid eventId);
    Task<RequestedCard?> GetByCodeAsync(string code);
    Task<RequestedCard?> GetByLinkAsync(string link);

    //Custom methods
    Task<RequestedCard> AddRequestedCardAsync(Guid requesterId,
        Guid eventId, string link, string code, int number); //To prevent duplicate
    Task<bool> CancelRequestedCardAsync(Guid requestId);
    Task<bool> IsRequestedCardExistsAsync(string code);
    Task<bool> IsRequestedCardUsedAsync(string code); //Is RC used by another user
}

