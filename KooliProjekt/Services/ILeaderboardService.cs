using KooliProjekt.Data;

namespace KooliProjekt.Services
{
    public interface ILeaderboardService
    {
        Task<PagedResult<Leaderboard>> List(int page, int pageSize);
        Task<Leaderboard> Get(int id);
        Task Save(Leaderboard list);
        Task Delete(int id);
    }
}