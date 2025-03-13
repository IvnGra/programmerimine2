using KooliProjekt.Data;
using KooliProjekt.Search;
using Microsoft.EntityFrameworkCore;

namespace KooliProjekt.Services
{
    public class TeamService : ITeamsService
    {
        private readonly ApplicationDbContext _context;

        public TeamService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<PagedResult<Team>> List(int page, int pageSize, TeamsSearch search = null)
        {
            var query = _context.Teams.AsQueryable();

            search = search ?? new TeamsSearch();

            if (!string.IsNullOrWhiteSpace(search.Keyword))
            {
                query = query.Where(team => team.TeamName.Contains(search.Keyword) || team.TeamDescription.Contains(search.Keyword)); ;
            }

            return await query.GetPagedAsync(page, pageSize);

        }

        public async Task<Team> Get(int id)
        {
            return await _context.Teams.FirstOrDefaultAsync(m => m.Id == id);
        }

        public async Task Save(Team list)
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
            var todoList = await _context.Teams.FindAsync(id);
            if (todoList != null)
            {
                _context.Teams.Remove(todoList);
                await _context.SaveChangesAsync();
            }
        }
    }
}
