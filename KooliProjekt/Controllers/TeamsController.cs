using KooliProjekt.Services;
using KooliProjekt.Models;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using KooliProjekt.Data;

namespace KooliProjekt.Controllers
{
    public class TeamsController : Controller
    {
        private readonly ITeamsService _teamService;

        public TeamsController(ITeamsService teamService)
        {
            _teamService = teamService;
        }

        public async Task<IActionResult> Index(int page = 1, TeamsIndexModel model = null)
        {
            model = model ?? new TeamsIndexModel();
            model.Data = await _teamService.List(page, 10, model.Search);
            return View(model);
        }
        public async Task<IActionResult> Details(int id)
        {
            var team = await _teamService.Get(id);
            if (team == null)
            {
                return NotFound();
            }
            return View(team);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Team team)
        {
            if (ModelState.IsValid)
            {
                await _teamService.Save(team);
                return RedirectToAction(nameof(Index));
            }
            return View(team);
        }

        public async Task<IActionResult> Edit(int id)
        {
            var team = await _teamService.Get(id);
            if (team == null)
            {
                return NotFound();
            }
            return View(team);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Team team)
        {
            if (id != team.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                await _teamService.Save(team);
                return RedirectToAction(nameof(Index));
            }
            return View(team);
        }

        public async Task<IActionResult> Delete(int id)
        {
            var team = await _teamService.Get(id);
            if (team == null)
            {
                return NotFound();
            }
            return View(team);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            await _teamService.Delete(id);
            return RedirectToAction(nameof(Index));
        }
    }
}
