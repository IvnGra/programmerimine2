using KooliProjekt.Services;
using KooliProjekt.Models;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using KooliProjekt.Data;

namespace KooliProjekt.Controllers
{
    public class MatchesController : Controller
    {
        private readonly IMatchesService _matchService;

        public MatchesController(IMatchesService matchService)
        {
            _matchService = matchService;
        }

        public async Task<IActionResult> Index(int page = 1, MatchesIndexModel model = null)
        {
            model = model ?? new MatchesIndexModel();
            model.Data = await _matchService.List(page, 10, model.Search);
            return View(model);
        }


        public async Task<IActionResult> Details(int id)
        {
            var match = await _matchService.Get(id);
            if (match == null)
            {
                return NotFound();
            }
            return View(match);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Match match)
        {
            if (ModelState.IsValid)
            {
                await _matchService.Save(match);
                return RedirectToAction(nameof(Index));
            }
            return View(match);
        }

        public async Task<IActionResult> Edit(int id)
        {
            var match = await _matchService.Get(id);
            if (match == null)
            {
                return NotFound();
            }
            return View(match);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Match match)
        {
            if (id != match.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                await _matchService.Save(match);
                return RedirectToAction(nameof(Index));
            }
            return View(match);
        }

        public async Task<IActionResult> Delete(int id)
        {
            var match = await _matchService.Get(id);
            if (match == null)
            {
                return NotFound();
            }
            return View(match);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            await _matchService.Delete(id);
            return RedirectToAction(nameof(Index));
        }
    }
}
