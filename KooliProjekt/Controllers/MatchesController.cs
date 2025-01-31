using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using KooliProjekt.Data;

namespace KooliProjekt.Controllers
{
    public class MatchesController : Controller
    {
        private readonly ApplicationDbContext _context;

        public MatchesController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Matches
        public async Task<IActionResult> Index(int page=5)
        {
            var applicationDbContext = _context.Matchs.Include(m => m.Team1).Include(m => m.Team2).Include(m => m.Tournament);
            return View(await applicationDbContext.GetPagedAsync(page, 5));
        }

        // GET: Matches/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var match = await _context.Matchs
                .Include(m => m.Team1)
                .Include(m => m.Team2)
                .Include(m => m.Tournament)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (match == null)
            {
                return NotFound();
            }

            return View(match);
        }

        // GET: Matches/Create
        public IActionResult Create()
        {
            ViewData["Team1Id"] = new SelectList(_context.Teams, "Id", "TeamName");
            ViewData["Team2Id"] = new SelectList(_context.Teams, "Id", "TeamName");
            ViewData["TournamentId"] = new SelectList(_context.Tournaments, "Id", "TournamentName");
            return View();
        }

        // POST: Matches/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,TournamentId,Team1Id,Team2Id,Match_time,Round,Team1_goals,Team2_goals")] Match match)
        {
            if (ModelState.IsValid)
            {
                _context.Add(match);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["Team1Id"] = new SelectList(_context.Teams, "Id", "TeamName", match.Team1Id);
            ViewData["Team2Id"] = new SelectList(_context.Teams, "Id", "TeamName", match.Team2Id);
            ViewData["TournamentId"] = new SelectList(_context.Tournaments, "Id", "TournamentName", match.TournamentId);
            return View(match);
        }

        // GET: Matches/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var match = await _context.Matchs.FindAsync(id);
            if (match == null)
            {
                return NotFound();
            }
            ViewData["Team1Id"] = new SelectList(_context.Teams, "Id", "TeamName", match.Team1Id);
            ViewData["Team2Id"] = new SelectList(_context.Teams, "Id", "TeamName", match.Team2Id);
            ViewData["TournamentId"] = new SelectList(_context.Tournaments, "Id", "TournamentName", match.TournamentId);
            return View(match);
        }

        // POST: Matches/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,TournamentId,Team1Id,Team2Id,Match_time,Round,Team1_goals,Team2_goals")] Match match)
        {
            if (id != match.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(match);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!MatchExists(match.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["Team1Id"] = new SelectList(_context.Teams, "Id", "TeamName", match.Team1Id);
            ViewData["Team2Id"] = new SelectList(_context.Teams, "Id", "TeamName", match.Team2Id);
            ViewData["TournamentId"] = new SelectList(_context.Tournaments, "Id", "TournamentName", match.TournamentId);
            return View(match);
        }

        // GET: Matches/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var match = await _context.Matchs
                .Include(m => m.Team1)
                .Include(m => m.Team2)
                .Include(m => m.Tournament)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (match == null)
            {
                return NotFound();
            }

            return View(match);
        }

        // POST: Matches/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var match = await _context.Matchs.FindAsync(id);
            if (match != null)
            {
                _context.Matchs.Remove(match);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool MatchExists(int id)
        {
            return _context.Matchs.Any(e => e.Id == id);
        }
    }
}
