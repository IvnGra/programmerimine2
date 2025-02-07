using KooliProjekt.Data;
using Microsoft.EntityFrameworkCore;

namespace KooliProjekt.Services
{
    public class TournamentService : ITournamentsService
    {
        private readonly ApplicationDbContext _context;

        public TournamentService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<PagedResult<Tournament>> List(int page, int pageSize)
        {
            return await _context.Tournaments.GetPagedAsync(page, 5);
        }

        public async Task<Tournament> Get(int id)
        {
            return await _context.Tournaments.FirstOrDefaultAsync(m => m.Id == id);
        }

        public async Task Save(Tournament list)
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
            var todoList = await _context.Tournaments.FindAsync(id);
            if (todoList != null)
            {
                _context.Tournaments.Remove(todoList);
                await _context.SaveChangesAsync();
            }
        }
    }
}
