using KooliProjekt.Services;
using KooliProjekt.Models;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using KooliProjekt.Models;
using KooliProjekt.Search;
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

        public async Task<IActionResult> Index(int page = 1, MatchesSearch search = null)
        {
            search = search ?? new MatchesSearch();

            var result = await _matchService.List(page, 5, search);

            var model = new MatchesIndexModel
            {
                Search = search,
                Data = result
            };

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
        public async Task<IActionResult> Create([Bind("Id,Name,Description,Creation_date")] Match match)
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
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,Description,Creation_date")] Match match)
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
