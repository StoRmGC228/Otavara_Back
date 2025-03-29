using Application.Interfaces;
using Domain.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    public class GoodController : ControllerBase
    {
        private readonly IGoodService _goodService;
        public GoodController(IGoodService goodService)
        {
            _goodService = goodService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllGoods()
        {
            var allGoods = await _goodService.GetAllGoods();
            return Ok(allGoods);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetGoodById(Guid id)
        {
            var searchedGood = await _goodService.GetGoodById(id);
            return Ok(searchedGood);
        }



        [HttpPost]
        public async Task<IActionResult> CreateGood([FromBody] Good newGood)
        {
            var createdGood = await _goodService.CreateGood(newGood);
            return CreatedAtAction(nameof(GetGoodById), new { id = createdGood.Id }, createdGood);
        }

        [HttpPut("{id}")]
        [Authorize]
        public async Task<IActionResult> UpdateGood(Guid id,[FromBody] Good updatedGood)
        {
            if(id != updatedGood.Id)
            {
                return BadRequest();
            }
            var searchedGood = await _goodService.GetGoodById(id);
            if(searchedGood == null)
            {
                return NotFound();
            }
            var result = await _goodService.UpdateGood(id, updatedGood);
            return Ok(result);
        }

        [HttpDelete("{id}")]
        [Authorize]
        public async Task<IActionResult> DeleteGood(Guid id)
        {
            var seachedGood = await _goodService.GetGoodById(id);
            if(seachedGood == null)
            {
                return NotFound();
            }
            await _goodService.DeleteGood(id);
            return NoContent();
        }

        [HttpGet("sorted/name")]
        public async Task<IActionResult> GetGoodsSortedByName([FromQuery] bool ascending = true)
        {
            var goods = await _goodService.GetAllSortedByNameAsync(ascending);
            return Ok(goods);
        }

        [HttpGet("sorted/quantity")]
        public async Task<IActionResult> GetGoodsSortedByQuantity([FromQuery] bool ascending = true)
        {
            var goods = await _goodService.GetAllSortedByQuantityAsync(ascending);
            return Ok(goods);
        }

        [HttpGet("sorted/time")]
        public async Task<IActionResult> GetGoodsSortedByTime([FromQuery] bool ascending = true)
        {
            var goods = _goodService.GetAllSortedByTimeAsync(ascending);
            return Ok(goods);
        }
    }
}
