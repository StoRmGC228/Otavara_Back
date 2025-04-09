using Application.Interfaces;
using Domain.Entities;

namespace Application.Services;

public class RequestedCardService : BaseService<RequestedCard>, IRequestedCardService
{
    private readonly IRequestedCardRepository _requestedCardRepository;
    public RequestedCardService(IRequestedCardRepository requestedCardRepository) : base(requestedCardRepository)
    {
        _requestedCardRepository = requestedCardRepository;
    }

    //Injected from RecuestedCardRepository
    public async Task<IEnumerable<RequestedCard>> GetByRequesterIdAsync(Guid requesterId)
    {
        return await _requestedCardRepository.GetByRequesterIdAsync(requesterId);
    }
    public async Task<IEnumerable<RequestedCard>> GetByEventIdAsync(Guid eventId)
    {
        return await _requestedCardRepository.GetByEventIdAsync(eventId);
    }
    public async Task<RequestedCard?> GetByCodeAsync(string code)
    {
        return await _requestedCardRepository.GetByCodeAsync(code);
    }
    public async Task<RequestedCard?> GetByLinkAsync(string link)
    {
        return await _requestedCardRepository.GetByLinkAsync(link);
    }

    //Custom methods
    public async Task<RequestedCard> AddRequestedCardAsync(Guid requesterId, Guid eventId, string link, string code, int number)
    {
        //To prevent duplicate
        if (!await IsRequestedCardExistsAsync(code))
        {
            throw new InvalidOperationException("A request for this card already exists.");
        }
        var requestedCard = new RequestedCard
        {
            Id = Guid.NewGuid(),
            RequesterId = requesterId,
            EventId = eventId,
            Link = link,
            Code = code,
            Number = number,
            RequestedDate = DateOnly.FromDateTime(DateTime.UtcNow)
        };
        return await _requestedCardRepository.AddAsync(requestedCard);
    }
    public async Task<bool> CancelRequestedCardAsync(Guid requestedCardId)
    {
        var requestedCard = await _requestedCardRepository.GetByIdAsync(requestedCardId);
        if (requestedCard == null)
        {
            return false;
        }
        await _requestedCardRepository.DeleteAsync(requestedCardId);
        return true;
    }
    public async Task<bool> IsRequestedCardExistsAsync(string code)
    {
        var existingRequestedCard = await _requestedCardRepository.GetByCodeAsync(code);
        return existingRequestedCard == null;
    }
    public async Task<bool> IsRequestedCardUsedAsync(string code)
    {
        if (await IsRequestedCardExistsAsync(code))
        {
            var requestedCard = await _requestedCardRepository.GetByCodeAsync(code);
            return requestedCard.Requester == null;
        }
        return false;
    }
}

