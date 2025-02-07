using KooliProjekt.Data;

namespace KooliProjekt.Services
{
    public interface ITournamentsService
    {
        Task<PagedResult<Tournament>> List(int page, int pageSize);
        Task<Tournament> Get(int id);
        Task Save(Tournament list);
        Task Delete(int id);
    }
}