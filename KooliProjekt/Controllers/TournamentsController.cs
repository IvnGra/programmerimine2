using KooliProjekt.Data;
using KooliProjekt.Models;
using KooliProjekt.Search;
using KooliProjekt.Services;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace KooliProjekt.Controllers
{
    public class TournamentsController : Controller
    {
        private readonly ITournamentsService _tournamentsService;

        public TournamentsController(ITournamentsService tournamentsService)
        {
            _tournamentsService = tournamentsService;
        }

        public async Task<IActionResult> Index(int page = 1, TournamentsSearch search = null)
        {
            search = search ?? new TournamentsSearch();

            var result = await _tournamentsService.List(page, 5, search);

            var model = new TournamentsIndexModel
            {
                Search = search,
                Data = result
            };

            return View(model);
        }
        public async Task<IActionResult> Details(int id)
        {
            var tournament = await _tournamentsService.Get(id);
            if (tournament == null)
            {
                return NotFound();
            }
            return View(tournament);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Tournament tournament)
        {
            if (ModelState.IsValid)
            {
                await _tournamentsService.Save(tournament);
                return RedirectToAction(nameof(Index));
            }
            return View(tournament);
        }

        public async Task<IActionResult> Edit(int id)
        {
            var tournament = await _tournamentsService.Get(id);
            if (tournament == null)
            {
                return NotFound();
            }
            return View(tournament);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Tournament tournament)
        {
            if (id != tournament.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                await _tournamentsService.Save(tournament);
                return RedirectToAction(nameof(Index));
            }
            return View(tournament);
        }

        public async Task<IActionResult> Delete(int id)
        {
            var tournament = await _tournamentsService.Get(id);
            if (tournament == null)
            {
                return NotFound();
            }
            return View(tournament);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            await _tournamentsService.Delete(id);
            return RedirectToAction(nameof(Index));
        }
    }
}
