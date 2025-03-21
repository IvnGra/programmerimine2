using KooliProjekt.Data;
using KooliProjekt.Search;

namespace KooliProjekt.Services
{
    public interface ITeamsService
    {
        Task<PagedResult<Team>> List(int page, int pageSize, TeamsSearch search = null);
        Task Create(Team team);
        Task<Team> Get(int id);
        Task Save(Team list);
        Task Delete(int id);
    }
}