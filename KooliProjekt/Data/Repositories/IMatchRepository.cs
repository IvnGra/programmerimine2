namespace KooliProjekt.Data.Repositories
{
    public interface IMatchRepository
    {
        Task<Match> Get(int id);
        Task<PagedResult<Match>> List(int page, int pageSize);
        Task Save(Match item);
        Task Delete(int id);
    }
}