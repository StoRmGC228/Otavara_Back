namespace API.Controllers;

using Application.Interfaces;
using Domain.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

public class GoodController : ControllerBase
{
    private readonly IGoodService _goodService;

    public GoodController(IGoodService goodService)
    {
        _goodService = goodService;
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
    public async Task<IActionResult> CreateGoodAsync([FromBody] Good newGood)
    {
        var createdGood = await _goodService.AddAsync(newGood);
        return Ok(createdGood);
    }

    [HttpPut("{id}")]
    [Authorize]
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

        var result = await _goodService.UpdateAsync(updatedGood);
        return Ok(result);
    }

    [HttpDelete("{id}")]
    [Authorize]
    public async Task<IActionResult> DeleteGoodAsync(Guid id)
    {
        var seachedGood = await _goodService.GetByIdAsync(id);
        if (seachedGood == null)
        {
            return NotFound();
        }

        await _goodService.DeleteAsync(id);
        return NoContent();
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
        var goods = _goodService.GetAllSortedByTimeAsync(ascending);
        return Ok(goods);
    }
}