using Application.Interfaces;
using Domain.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    public class GoodController : ControllerBase
    {
        private readonly IBaseService<Good> _baseService;
        private readonly IGoodService _goodService;
        public GoodController(IBaseService<Good> baseService, IGoodService goodService)
        {
            _baseService = baseService;
            _goodService = goodService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllGoodsAsync()
        {
            var allGoods = await _baseService.GetAllAsync();
            return Ok(allGoods);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetGoodByIdAsync(Guid id)
        {
            var searchedGood = await _baseService.GetByIdAsync(id);
            return Ok(searchedGood);
        }

        [HttpPost]
        public async Task<IActionResult> CreateGoodAsync([FromBody] Good newGood)
        {
            var createdGood = await _baseService.AddAsync(newGood);
            return CreatedAtAction(nameof(GetGoodByIdAsync), new { id = createdGood.Id }, createdGood);
        }

        [HttpPut("{id}")]
        [Authorize]
        public async Task<IActionResult> UpdateGoodAsync(Guid id, [FromBody] Good updatedGood)
        {
            if (id != updatedGood.Id)
            {
                return BadRequest();
            }
            var searchedGood = await _baseService.GetByIdAsync(id);
            if (searchedGood == null)
            {
                return NotFound();
            }
            var result = await _baseService.UpdateAsync(updatedGood);
            return Ok(result);
        }

        [HttpDelete("{id}")]
        [Authorize]
        public async Task<IActionResult> DeleteGoodAsync(Guid id)
        {
            var seachedGood = await _baseService.GetByIdAsync(id);
            if (seachedGood == null)
            {
                return NotFound();
            }
            await _baseService.DeleteAsync(id);
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
}
