using KooliProjekt.Data;
using KooliProjekt.Search;

namespace KooliProjekt.Services
{
    public interface IMatchesService
    {
        Task<PagedResult<Match>> List(int page, int pageSize, MatchesSearch search = null);
        Task<Match> Get(int id);
        Task Save(Match list);
        Task Delete(int id);
    }
}