using KooliProjekt.Data;
using KooliProjekt.Search;
using Microsoft.EntityFrameworkCore;
using static NuGet.Packaging.PackagingConstants;

namespace KooliProjekt.Services
{
    public class MatchService : IMatchesService
    {
        private readonly ApplicationDbContext _context;

        public MatchService(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task Create(Match match)
        {
            _context.Matchs.Add(match);
            await _context.SaveChangesAsync();
        }

        public async Task<PagedResult<Match>> List(int page, int pageSize, MatchesSearch search = null)
        {
            var query = _context.Matchs.AsQueryable();

            search = search ?? new MatchesSearch();

            if (!string.IsNullOrWhiteSpace(search.Keyword))
            {
                query = query.Where(match => match.Name.Contains(search.Keyword) || match.Description.Contains(search.Keyword));;
            }

            return await query.GetPagedAsync(page, pageSize);
                
        }

        public async Task<Match> Get(int id)
        {
            return await _context.Matchs.FirstOrDefaultAsync(m => m.Id == id);
        }

        public async Task Save(Match match)
        {
            if (match.Id == 0)
            {
                _context.Add(match);
            }
            else
            {
                _context.Update(match);
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
