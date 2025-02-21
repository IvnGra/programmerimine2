using KooliProjekt.Data;
using KooliProjekt.Search;

namespace KooliProjekt.Services
{
    public interface ILeaderboardService
    {
        Task<PagedResult<Leaderboard>> List(int page, int pageSize, LeaderboardsSearch search = null);
        Task<Leaderboard> Get(int id);
        Task Save(Leaderboard list);
        Task Delete(int id);
    }
}