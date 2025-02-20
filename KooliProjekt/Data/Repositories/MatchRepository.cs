
using Microsoft.EntityFrameworkCore;
using static NuGet.Packaging.PackagingConstants;

namespace KooliProjekt.Data.Repositories
{
    public class MatchRepository : BaseRepository<Match>, IMatchRepository
    {
        public MatchRepository(ApplicationDbContext context) : base(context)
        {
        }

    }
}