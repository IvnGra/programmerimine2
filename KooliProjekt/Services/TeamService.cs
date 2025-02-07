using KooliProjekt.Data;
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

        public async Task<PagedResult<Team>> List(int page, int pageSize)
        {
            return await _context.Teams.GetPagedAsync(page, 5);
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
