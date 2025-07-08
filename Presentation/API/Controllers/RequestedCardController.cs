namespace API.Controllers;

using Application.Interfaces;
using AutoMapper;
using Domain.DtoEntities;
using Domain.Entities;
using Microsoft.AspNetCore.Mvc;

[Route("api/[controller]")]
[ApiController]
public class RequestedCardController : ControllerBase
{
    private readonly IMapper _mapper;
    private readonly IRequestedCardService _requestedCardService;

    public RequestedCardController(IRequestedCardService requestedCardService, IMapper mapper)
    {
        _requestedCardService = requestedCardService;
        _mapper = mapper;
    }

    //BaseService methods
    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateRequestedCard(Guid id, [FromBody] Card updatedRequestedCard)
    {
        if (id != updatedRequestedCard.Id)
        {
            return BadRequest();
        }

        var existingRequestedCard = await _requestedCardService.GetByIdAsync(id);
        if (existingRequestedCard == null)
        {
            return NotFound();
        }

        await _requestedCardService.UpdateAsync(updatedRequestedCard);
        return Ok();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteRequestedCard(Guid id)
    {
        var existingRequestedCard = await _requestedCardService.GetByIdAsync(id);
        if (existingRequestedCard == null)
        {
            return NotFound();
        }

        await _requestedCardService.DeleteAsync(id);
        return NoContent();
    }

    [HttpGet]
    public async Task<IActionResult> GetAllRequestedCards()
    {
        var requestedCards = await _requestedCardService.GetAllAsync();
        var mappedCards = _mapper.Map<IEnumerable<CardDto>>(requestedCards);
        return Ok(mappedCards);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetRequestedCardById(Guid id)
    {
        var requestedCard = await _requestedCardService.GetByIdAsync(id);
        if (requestedCard == null)
        {
            return NotFound();
        }

        var mappedCard = _mapper.Map<CardDto>(requestedCard);

        return Ok(mappedCard);
    }

    [HttpPost]
    public async Task<IActionResult> CreateRequestedCard([FromBody] CardDto newRequestedCard)
    {
        var mappedCard = _mapper.Map<Card>(newRequestedCard);
        await _requestedCardService.AddAsync(mappedCard);

        return Ok();
    }

    [HttpGet("isExist/{id}")]
    public async Task<IActionResult> IsRequestedCardExists(Guid id)
    {
        var result = await _requestedCardService.IsRequestedCardExistsAsync(id);
        return Ok(result);
    }
}