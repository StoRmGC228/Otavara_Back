namespace Application.Interfaces;

using Domain.Entities;

public interface IRequestedCardRepository : IBaseRepository<RequestedCard>
{
    Task<IEnumerable<RequestedCard>> GetByRequesterIdAsync(Guid requesterId);
    Task<IEnumerable<RequestedCard>> GetByEventIdAsync(Guid eventId);
    Task<RequestedCard?> GetByCodeAsync(string code);
    Task<RequestedCard?> GetByLinkAsync(string link);
}