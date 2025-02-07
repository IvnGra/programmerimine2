using KooliProjekt.Services;
using KooliProjekt.Models;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using KooliProjekt.Data;

namespace KooliProjekt.Controllers
{
    public class PredictionsController : Controller
    {
        private readonly IPredictionService _predictionService;

        public PredictionsController(IPredictionService predictionService)
        {
            _predictionService = predictionService;
        }


        public async Task<IActionResult> Details(int id)
        {
            var prediction = await _predictionService.Get(id);
            if (prediction == null)
            {
                return NotFound();
            }
            return View(prediction);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Prediction prediction)
        {
            if (ModelState.IsValid)
            {
                await _predictionService.Save(prediction);
                return RedirectToAction(nameof(Index));
            }
            return View(prediction);
        }

        public async Task<IActionResult> Edit(int id)
        {
            var prediction = await _predictionService.Get(id);
            if (prediction == null)
            {
                return NotFound();
            }
            return View(prediction);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Prediction prediction)
        {
            if (id != prediction.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                await _predictionService.Save(prediction);
                return RedirectToAction(nameof(Index));
            }
            return View(prediction);
        }

        public async Task<IActionResult> Delete(int id)
        {
            var prediction = await _predictionService.Get(id);
            if (prediction == null)
            {
                return NotFound();
            }
            return View(prediction);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            await _predictionService.Delete(id);
            return RedirectToAction(nameof(Index));
        }
    }
}
