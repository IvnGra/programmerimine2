using KooliProjekt.Data;
using KooliProjekt.Search;
using Microsoft.EntityFrameworkCore;

namespace KooliProjekt.Services
{
    public class PredictionsService : IPredictionService
    {
        private readonly ApplicationDbContext _context;

        public PredictionsService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<PagedResult<Prediction>> List(int page, int pageSize, PredictionsSearch search = null)
        {
            var query = _context.Predictions.AsQueryable();

            search = search ?? new PredictionsSearch();

            if (!string.IsNullOrWhiteSpace(search.Keyword))
            {
                query = query.Where(prediction => prediction.Name.Contains(search.Keyword) || prediction.Points.Contains(search.Keyword)); ;
            }

            return await query.GetPagedAsync(page, pageSize);

        }

        public async Task<Prediction> Get(int id)
        {
            return await _context.Predictions.FirstOrDefaultAsync(m => m.Id == id);
        }

        public async Task Save(Prediction list)
        {
            if (list.Id == 0)
            {
                _context.Add(list);
            }
            else
            {
                _context.Update(list);
            }

            await _context.SaveChangesAsync();
        }

        public async Task Delete(int id)
        {
            var todoList = await _context.Predictions.FindAsync(id);
            if (todoList != null)
            {
                _context.Predictions.Remove(todoList);
                await _context.SaveChangesAsync();
            }
        }
    }
}
