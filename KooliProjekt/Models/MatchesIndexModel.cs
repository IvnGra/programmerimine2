using KooliProjekt.Data;
using KooliProjekt.Search;
using static NuGet.Packaging.PackagingConstants;

namespace KooliProjekt.Models
{
    public class MatchesIndexModel
    {
        public MatchesSearch Search { get; set; }
        public PagedResult<Match> Data { get; set; }
    }
}
