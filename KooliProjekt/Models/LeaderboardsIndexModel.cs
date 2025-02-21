using KooliProjekt.Data;
using KooliProjekt.Search;
using static NuGet.Packaging.PackagingConstants;

namespace KooliProjekt.Models
{
    public class LeaderboardsIndexModel
    {
        public LeaderboardsSearch Search { get; set; }
        public PagedResult<Leaderboard> Data { get; set; }
    }
}
