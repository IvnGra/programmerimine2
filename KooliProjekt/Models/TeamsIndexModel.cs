using KooliProjekt.Search;
using KooliProjekt.Data;

namespace KooliProjekt.Models
{
    public class TeamsIndexModel
    {
        public TeamsSearch Search { get; set; }
        public PagedResult<Team> Data { get; set; }
    }
}

   