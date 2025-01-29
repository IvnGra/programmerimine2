using KooliProjekt.Search;
using KooliProjekt.Data;

namespace KooliProjekt.Models
{
    public class TournamentsIndexModel
    {
        public TournamentsSearch Search { get; set; }
        public PagedResult<Tournament> Data { get; set; }
    }
}