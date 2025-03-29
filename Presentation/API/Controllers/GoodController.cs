using Application.Interfaces;
using Domain.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    public class GoodController : Controller
    {
        private readonly IGoodService _goodService;
        public GoodController(IGoodService goodService)
        {
            _goodService = goodService;
        }

        [HttpPost]
        public async Task<IActionResult> CreateGood([FromBody] Good good)
        {
            var createdGood = await _goodService.CreateGood(good);
            return Ok(createdGood);
        }


        // GET: GoodController
        public ActionResult Index()
        {
            return View();
        }

        // GET: GoodController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: GoodController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: GoodController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: GoodController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: GoodController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: GoodController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: GoodController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}
