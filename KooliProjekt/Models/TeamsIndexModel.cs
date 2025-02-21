using KooliProjekt.Data;
using KooliProjekt.Search;
using static NuGet.Packaging.PackagingConstants;

namespace KooliProjekt.Models
{
    public class TeamsIndexModel
    {
        public TeamsSearch Search { get; set; }
        public PagedResult<Team> Data { get; set; }
    }
}
