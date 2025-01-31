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
    public class PredictionsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public PredictionsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Predictions
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.Predictions.Include(p => p.Match).Include(p => p.User);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: Predictions/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var prediction = await _context.Predictions
                .Include(p => p.Match)
                .Include(p => p.User)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (prediction == null)
            {
                return NotFound();
            }

            return View(prediction);
        }

        // GET: Predictions/Create
        public IActionResult Create()
        {
            ViewData["MatchId"] = new SelectList(_context.Matchs, "Id", "Id");
            ViewData["UserId"] = new SelectList(_context.User, "Id", "Username");
            return View();
        }

        // POST: Predictions/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,UserId,MatchId,Team1_predicted_goals,Team2_predicted_goals,PointsEarned")] Prediction prediction)
        {
            if (ModelState.IsValid)
            {
                _context.Add(prediction);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["MatchId"] = new SelectList(_context.Matchs, "Id", "Id", prediction.MatchId);
            ViewData["UserId"] = new SelectList(_context.User, "Id", "Username", prediction.UserId);
            return View(prediction);
        }

        // GET: Predictions/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var prediction = await _context.Predictions.FindAsync(id);
            if (prediction == null)
            {
                return NotFound();
            }
            ViewData["MatchId"] = new SelectList(_context.Matchs, "Id", "Id", prediction.MatchId);
            ViewData["UserId"] = new SelectList(_context.User, "Id", "Username", prediction.UserId);
            return View(prediction);
        }

        // POST: Predictions/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,UserId,MatchId,Team1_predicted_goals,Team2_predicted_goals,PointsEarned")] Prediction prediction)
        {
            if (id != prediction.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(prediction);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PredictionExists(prediction.Id))
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
            ViewData["MatchId"] = new SelectList(_context.Matchs, "Id", "Id", prediction.MatchId);
            ViewData["UserId"] = new SelectList(_context.User, "Id", "Username", prediction.UserId);
            return View(prediction);
        }

        // GET: Predictions/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var prediction = await _context.Predictions
                .Include(p => p.Match)
                .Include(p => p.User)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (prediction == null)
            {
                return NotFound();
            }

            return View(prediction);
        }

        // POST: Predictions/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var prediction = await _context.Predictions.FindAsync(id);
            if (prediction != null)
            {
                _context.Predictions.Remove(prediction);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool PredictionExists(int id)
        {
            return _context.Predictions.Any(e => e.Id == id);
        }
    }
}
