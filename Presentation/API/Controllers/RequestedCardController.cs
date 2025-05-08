namespace API.Controllers;

using Application.Interfaces;
using Domain.Entities;
using Microsoft.AspNetCore.Mvc;

[Route("api/[controller]")]
[ApiController]
public class RequestedCardController : ControllerBase
{
    private readonly IRequestedCardService _requestedCardService;

    public RequestedCardController(IRequestedCardService requestedCardService)
    {
        _requestedCardService = requestedCardService;
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
        return Ok(requestedCards);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetRequestedCardById(Guid id)
    {
        var requestedCard = await _requestedCardService.GetByIdAsync(id);
        if (requestedCard == null)
        {
            return NotFound();
        }

        return Ok(requestedCard);
    }

    [HttpPost]
    public async Task<IActionResult> CreateRequestedCard([FromBody] Card newRequestedCard)
    {
        var result = await _requestedCardService.AddAsync(newRequestedCard);
        return CreatedAtAction(nameof(GetRequestedCardById), new { id = result.Id }, result);
    }

    //RequestedCardService methods : Injected from RequestedCardService
    [HttpGet("{code}")]
    public async Task<IActionResult> GetRequestedCardByCode(string code)
    {
        var requestedCard = await _requestedCardService.GetByCodeAsync(code);
        if (requestedCard == null)
        {
            return NotFound();
        }

        return Ok(requestedCard);
    }

    [HttpGet("isExist/{code}")]
    public async Task<IActionResult> IsRequestedCardExists(string code)
    {
        var result = await _requestedCardService.IsRequestedCardExistsAsync(code);
        return Ok(result);
    }
}