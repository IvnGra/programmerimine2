using KooliProjekt.Services;
using KooliProjekt.Models;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using KooliProjekt.Data;

namespace KooliProjekt.Controllers
{
    public class LeaderboardsController : Controller
    {
        private readonly ILeaderboardService _leaderboardService;

        public LeaderboardsController(ILeaderboardService leaderboardService)
        {
            _leaderboardService = leaderboardService;
        }

        public async Task<IActionResult> Details(int id)
        {
            var leaderboard = await _leaderboardService.Get(id);
            if (leaderboard == null)
            {
                return NotFound();
            }
            return View(leaderboard);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,TournamentId,UserId,Total_id")] Leaderboard leaderboard)
        {
            if (ModelState.IsValid)
            {
                await _leaderboardService.Save(leaderboard);
                return RedirectToAction(nameof(Index));
            }
            return View(leaderboard);
        }

        public async Task<IActionResult> Edit(int id)
        {
            var leaderboard = await _leaderboardService.Get(id);
            if (leaderboard == null)
            {
                return NotFound();
            }
            return View(leaderboard);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Leaderboard leaderboard)
        {
            if (id != leaderboard.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                await _leaderboardService.Save(leaderboard);
                return RedirectToAction(nameof(Index));
            }
            return View(leaderboard);
        }

        public async Task<IActionResult> Delete(int id)
        {
            var leaderboard = await _leaderboardService.Get(id);
            if (leaderboard == null)
            {
                return NotFound();
            }
            return View(leaderboard);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            await _leaderboardService.Delete(id);
            return RedirectToAction(nameof(Index));
        }
    }
}
