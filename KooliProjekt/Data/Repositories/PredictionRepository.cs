
using Microsoft.EntityFrameworkCore;
using static NuGet.Packaging.PackagingConstants;

namespace KooliProjekt.Data.Repositories
{
    public class PredictionRepository : BaseRepository<Prediction>, IPredicitonRepository
    {
        public PredictionRepository(ApplicationDbContext context) : base(context)
        {
        }

    }
}