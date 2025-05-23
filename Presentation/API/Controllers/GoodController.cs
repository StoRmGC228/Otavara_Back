namespace API.Controllers;

using Application.Interfaces;
using AutoMapper;
using Domain.DtoEntities;
using Domain.Entities;
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

        if (User.IsInRole("Admin"))
        {
            var mappedGoods = _mapper.Map<IEnumerable<GoodAdminDto>>(allGoods);
            return Ok(mappedGoods);
        }
        else
        {
            var mappedGoods = _mapper.Map<IEnumerable<GoodDto>>(allGoods);
            return Ok(mappedGoods);
        }
    }
    
    [HttpGet("{id}")]
    public async Task<IActionResult> GetGoodByIdAsync(Guid id)
    {
        var searchedGood = await _goodService.GetByIdAsync(id);

        if (User.IsInRole("Admin"))
        {
            var mappedGood = _mapper.Map<GoodAdminDto>(searchedGood);
            return Ok(mappedGood);
        }
        else
        {
            var mappedGood = _mapper.Map<GoodDto>(searchedGood);
            return Ok(mappedGood);
        }
    }

    [HttpPost]
    public async Task<IActionResult> CreateGoodAsync([FromBody] GoodCreationDto newGoodAdmin)
    {
        var receivedGood = _mapper.Map<Good>(newGoodAdmin);
        var createdGood = await _goodService.AddAsync(receivedGood);
        return Ok(createdGood);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateGoodAsync(Guid id, [FromBody] GoodAdminDto updatedGood)
    {
        if (id != updatedGood.Id)
            return NotFound();

        var searchedGood = await _goodService.GetByIdAsync(id);
        if (searchedGood == null)
        {
            return NotFound();
        }

        var mappedGood = _mapper.Map<Good>(updatedGood);
        await _goodService.UpdateAsync(mappedGood, id);
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