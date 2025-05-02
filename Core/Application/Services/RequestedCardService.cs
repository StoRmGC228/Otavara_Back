using Application.Interfaces;
using AutoMapper;
using Domain.DtoEntities;
using Domain.Entities;

namespace Application.Services;

public class RequestedCardService : BaseService<Card>, IRequestedCardService
{
    private readonly IRequestedCardRepository _requestedCardRepository;
    private readonly IMapper _mapper;

    public RequestedCardService(IRequestedCardRepository requestedCardRepository, IMapper mapper) : base(requestedCardRepository, mapper)
    {
        _requestedCardRepository = requestedCardRepository;
        _mapper = mapper;
    }

    public async Task<Card> GetByCodeAsync(string code)
    {
        return await _requestedCardRepository.GetByCodeAsync(code);
    }

    public async Task<Card> AddRequestedCardAsync(CardDto card)
    {
        if (!await IsRequestedCardExistsAsync(card.Code))
        {
            throw new InvalidOperationException("A request for this card already exists.");
        }
        var requestedCard = new Card
        {
            Id = new Guid(),
            Code = card.Code,
            CardMarketLink = card.CardMarketLink,
            CardHoarderLink = card.CardHoarderLink,
            ImageLink = card.ImageLink,
            Name = card.Name,
            TcgPlayerLink = card.TcgPlayerLink,
        };
        return await _requestedCardRepository.AddAsync(requestedCard);
    }
    public async Task<bool> IsRequestedCardExistsAsync(string code)
    {
        var existingRequestedCard = await _requestedCardRepository.GetByCodeAsync(code);
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
        return await IsRequestedCardExistsAsync(entity.Code) ? entity : await _requestedCardRepository.AddAsync(entity);
    }

    public async Task<Card> UpdateAsync(Guid id,Card entity)
    {
        return await _requestedCardRepository.UpdateAsync(id, entity);
    }
}

