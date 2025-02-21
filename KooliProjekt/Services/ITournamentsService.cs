using KooliProjekt.Data;
using KooliProjekt.Search;

namespace KooliProjekt.Services
{
    public interface ITournamentsService
    {
        Task<PagedResult<Tournament>> List(int page, int pageSize, TournamentsSearch search = null);
        Task<Tournament> Get(int id);
        Task Save(Tournament list);
        Task Delete(int id);
    }
}