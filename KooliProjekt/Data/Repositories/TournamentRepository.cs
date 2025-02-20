
using Microsoft.EntityFrameworkCore;
using static NuGet.Packaging.PackagingConstants;

namespace KooliProjekt.Data.Repositories
{
    public class TournamentRepository : BaseRepository<Tournament>, ITournamentRepository
    {
        public TournamentRepository(ApplicationDbContext context) : base(context)
        {
        }

    }
}