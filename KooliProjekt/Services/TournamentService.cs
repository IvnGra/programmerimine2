using KooliProjekt.Data;
using KooliProjekt.Search;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;

namespace KooliProjekt.Services
{
    public class TournamentService : ITournamentsService
    {
        private readonly ApplicationDbContext _context;

        public TournamentService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<PagedResult<Tournament>> List(int page, int pageSize, TournamentsSearch search = null)
        {
            var query = _context.Tournaments.AsQueryable();

            search = search ?? new TournamentsSearch();

            if (!string.IsNullOrWhiteSpace(search.Keyword))
            {
                query = query.Where(tournament => tournament.TournamentName.Contains(search.Keyword)); ;
            }

            return await query.GetPagedAsync(page, pageSize);

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
