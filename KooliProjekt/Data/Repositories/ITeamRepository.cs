namespace KooliProjekt.Data.Repositories
{
    public interface ITeamRepository
    {
        Task<Team> Get(int id);
        Task<PagedResult<Team>> List(int page, int pageSize);
        Task Save(Team item);
        Task Delete(int id);
    }
}