using KooliProjekt.Data;
using KooliProjekt.Search;

namespace KooliProjekt.Models
{
    public class TournamentsIndexModel
    {
        public TournamentsSearch Search { get; set; }
        public PagedResult<Tournament> Data { get; set; }
    }
}
