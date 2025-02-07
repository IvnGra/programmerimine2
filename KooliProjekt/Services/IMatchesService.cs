using KooliProjekt.Data;

namespace KooliProjekt.Services
{
    public interface IMatchesService
    {
        Task<PagedResult<Match>> List(int page, int pageSize);
        Task<Match> Get(int id);
        Task Save(Match list);
        Task Delete(int id);
    }
}