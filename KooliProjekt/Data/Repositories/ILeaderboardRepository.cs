namespace KooliProjekt.Data.Repositories
{
    public interface ILeaderboardRepository
    {
        Task<Leaderboard> Get(int id);
        Task<PagedResult<Leaderboard>> List(int page, int pageSize);
        Task Save(Leaderboard item);
        Task Delete(int id);
    }
}