namespace KooliProjekt.Data.Repositories
{
    public interface ITournamentRepository
    {
        Task<Tournament> Get(int id);
        Task<PagedResult<Tournament>> List(int page, int pageSize);
        Task Save(Tournament item);
        Task Delete(int id);
    }
}