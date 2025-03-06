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
        // GET: Tournaments

        // GET: Tournaments/Details/5
        public IActionResult Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tournament = _tournamentsService.Get(id.Value);
            if (tournament == null)
            {
                return NotFound();
            }

            return View(tournament);
        }

        // GET: Tournaments/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Tournaments/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create([Bind("Id,TournamentName,StartDate,EndDate")] Tournament tournament)
        {
            if (ModelState.IsValid)
            {
                _tournamentsService.Save(tournament);
                return RedirectToAction(nameof(Index));
            }
            return View(tournament);
        }

        // GET: Tournaments/Edit/5
        public IActionResult Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tournament = _tournamentsService.Get(id.Value);
            if (tournament == null)
            {
                return NotFound();
            }

            return View(tournament);
        }

        // POST: Tournaments/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id, [Bind("Id,TournamentName,StartDate,EndDate")] Tournament tournament)
        {
            if (id != tournament.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                _tournamentsService.Save(tournament);
                return RedirectToAction(nameof(Index));
            }
            return View(tournament);
        }

        // GET: Tournaments/Delete/5
        public IActionResult Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tournament = _tournamentsService.Get(id.Value);
            if (tournament == null)
            {
                return NotFound();
            }

            return View(tournament);
        }

        // POST: Tournaments/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            _tournamentsService.Delete(id);
            return RedirectToAction(nameof(Index));
        }
    }
}
