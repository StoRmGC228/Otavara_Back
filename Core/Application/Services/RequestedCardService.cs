namespace Application.Services;

using AutoMapper;
using Domain.DtoEntities;
using Domain.Entities;
using Interfaces;

public class RequestedCardService : BaseService<Card>, IRequestedCardService
{
    private readonly IMapper _mapper;
    private readonly IRequestedCardRepository _requestedCardRepository;

    public RequestedCardService(IRequestedCardRepository requestedCardRepository, IMapper mapper) : base(
        requestedCardRepository, mapper)
    {
        _requestedCardRepository = requestedCardRepository;
        _mapper = mapper;
    }

    public async Task<bool> IsRequestedCardExistsAsync(Guid id)
    {
        var existingRequestedCard = await _requestedCardRepository.GetByIdAsync(id);
        return existingRequestedCard != null;
    }

    public async Task<IEnumerable<Card>> GetAllAsync()
    {
        return await _requestedCardRepository.GetAllAsync();
    }

    public async Task<Card?> GetByIdAsync(Guid id)
    {
        return await _requestedCardRepository.GetByIdAsync(id);
    }

    public async Task<PaginatedDto<Card>> GetPaginateAsync(int pageSize, int pageNumber)
    {
        return await _requestedCardRepository.GetPaginatedAsync(pageSize, pageNumber);
    }

    public async Task<Card> AddAsync(Card entity)
    {
        return await IsRequestedCardExistsAsync(entity.Id) ? entity : await _requestedCardRepository.AddAsync(entity);
    }

    public async Task<Card> AddRequestedCardAsync(CardDto card)
    {
        if (!await IsRequestedCardExistsAsync(Guid.Parse(card.Id)))
        {
            throw new InvalidOperationException("A request for this card already exists.");
        }

        var requestedCard = _mapper.Map<Card>(card);
        return await _requestedCardRepository.AddAsync(requestedCard);
    }

    public async Task<Card> UpdateAsync(Guid id, Card entity)
    {
        return await _requestedCardRepository.UpdateAsync(id, entity);
    }
}