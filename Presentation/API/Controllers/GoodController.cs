namespace API.Controllers;

using Application.Interfaces;
using AutoMapper;
using Domain.DtoEntities;
using Domain.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

[Route("api/[controller]")]
[ApiController]
public class GoodController : ControllerBase
{
    private readonly IGoodService _goodService;
    private readonly IMapper _mapper;

    public GoodController(IGoodService goodService, IMapper mapper)
    {
        _goodService = goodService;
        _mapper = mapper;
    }

    [HttpGet]
    public async Task<IActionResult> GetAllGoodsAsync()
    {
        var allGoods = await _goodService.GetAllAsync();
        return Ok(allGoods);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetGoodByIdAsync(Guid id)
    {
        var searchedGood = await _goodService.GetByIdAsync(id);
        return Ok(searchedGood);
    }

    [HttpPost]
    public async Task<IActionResult> CreateGoodAsync([FromBody] GoodCreationDto newGood)
    {
        var receivedGood = _mapper.Map<Good>(newGood);
        var createdGood = await _goodService.AddAsync(receivedGood);
        return Ok(createdGood);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateGoodAsync(Guid id, [FromBody] Good updatedGood)
    {
        if (id != updatedGood.Id)
        {
            return BadRequest();
        }

        var searchedGood = await _goodService.GetByIdAsync(id);
        if (searchedGood == null)
        {
            return NotFound();
        }

        await _goodService.UpdateAsync(updatedGood, id);
        return Ok();

    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteGoodAsync(Guid id)
    {
        var seachedGood = await _goodService.GetByIdAsync(id);
        if (seachedGood == null)
        {
            return NotFound();
        }

        await _goodService.DeleteAsync(id);
        return Ok();
    }

    [HttpGet("sorted/name")]
    public async Task<IActionResult> GetGoodsSortedByNameAsync([FromQuery] bool ascending = true)
    {
        var goods = await _goodService.GetAllSortedByNameAsync(ascending);
        return Ok(goods);
    }

    [HttpGet("sorted/quantity")]
    public async Task<IActionResult> GetGoodsSortedByQuantityAsync([FromQuery] bool ascending = true)
    {
        var goods = await _goodService.GetAllSortedByQuantityAsync(ascending);
        return Ok(goods);
    }

    [HttpGet("sorted/time")]
    public async Task<IActionResult> GetGoodsSortedByTimeAsync([FromQuery] bool ascending = true)
    {
        var goods = await _goodService.GetAllSortedByTimeAsync(ascending);
        return Ok(goods);
    }
}