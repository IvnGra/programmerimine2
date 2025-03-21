using KooliProjekt.Data;
using KooliProjekt.Search;
using Microsoft.EntityFrameworkCore;
using static NuGet.Packaging.PackagingConstants;

namespace KooliProjekt.Services
{
    public class LeaderboardService : ILeaderboardService
    {
        private readonly ApplicationDbContext _context;

        public LeaderboardService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task Create(Leaderboard leaderboard)
        {
            _context.Leaderboards.Add(leaderboard);
            await _context.SaveChangesAsync();
        }

        public async Task<PagedResult<Leaderboard>> List(int page, int pageSize, LeaderboardsSearch search = null)
        {
            var query = _context.Leaderboards.AsQueryable();

            search = search ?? new LeaderboardsSearch();

            if (!string.IsNullOrWhiteSpace(search.Keyword))
            {
                query = query.Where(leaderboard => leaderboard.Name.Contains(search.Keyword) || leaderboard.place.Contains(search.Keyword)); ;
            }

            return await query.GetPagedAsync(page, pageSize);

        }

        public async Task<Leaderboard> Get(int id)
        {
            return await _context.Leaderboards.FirstOrDefaultAsync(m => m.Id == id);
        }

        public async Task Save(Leaderboard list)
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
            var todoList = await _context.Leaderboards.FindAsync(id);
            if (todoList != null)
            {
                _context.Leaderboards.Remove(todoList);
                await _context.SaveChangesAsync();
            }
        }
    }
}
