
using Microsoft.EntityFrameworkCore;
using static NuGet.Packaging.PackagingConstants;

namespace KooliProjekt.Data.Repositories
{
    public class LeaderboardRepository : BaseRepository<Leaderboard>, ILeaderboardRepository
    {
        public LeaderboardRepository(ApplicationDbContext context) : base(context)
        {
        }

    }
}