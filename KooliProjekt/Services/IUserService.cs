using KooliProjekt.Data;
using KooliProjekt.Search;
using System.Threading.Tasks;

namespace KooliProjekt.Services
{
    public interface IUserService
    {
        Task<PagedResult<User>> List(int page, int pageSize, UsersSearch search = null);
        Task<User> Get(int id);
        Task Save(User user);
        Task Delete(int id);
    }
}