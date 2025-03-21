using KooliProjekt.Data;
using KooliProjekt.Search;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;

namespace KooliProjekt.Services
{
    public interface IMatchesService
    {
        Task<PagedResult<Match>> List(int page, int pageSize, MatchesSearch search = null);
        Task Create(Match match);
        Task<Match> Get(int id);
        Task Save(Match list);
        Task Delete(int id);
    }
}