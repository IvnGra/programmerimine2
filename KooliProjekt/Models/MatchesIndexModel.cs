using KooliProjekt.Data;
using KooliProjekt.Search;

namespace KooliProjekt.Models
{
    public class MatchesIndexModel
    {
        public MatchesSearch Search { get; set; }
        public PagedResult<Match> Data { get; set; }
    }
}
