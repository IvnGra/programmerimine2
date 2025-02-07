using KooliProjekt.Data;
using Microsoft.EntityFrameworkCore;

namespace KooliProjekt.Services
{
    public class MatchService : IMatchesService
    {
        private readonly ApplicationDbContext _context;

        public MatchService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<PagedResult<Match>> List(int page, int pageSize)
        {
            return await _context.Matchs.GetPagedAsync(page, 5);
        }

        public async Task<Match> Get(int id)
        {
            return await _context.Matchs.FirstOrDefaultAsync(m => m.Id == id);
        }

        public async Task Save(Match list)
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
            var todoList = await _context.Matchs.FindAsync(id);
            if (todoList != null)
            {
                _context.Matchs.Remove(todoList);
                await _context.SaveChangesAsync();
            }
        }
    }
}
