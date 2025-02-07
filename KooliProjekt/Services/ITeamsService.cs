using KooliProjekt.Data;

namespace KooliProjekt.Services
{
    public interface ITeamsService
    {
        Task<PagedResult<Team>> List(int page, int pageSize);
        Task<Team> Get(int id);
        Task Save(Team list);
        Task Delete(int id);
    }
}