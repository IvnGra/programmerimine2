using KooliProjekt.Search;
using KooliProjekt.Data;

namespace KooliProjekt.Models
{
    public class UsersIndexModel
    {
        public UsersSearch Search { get; set; }
        public PagedResult<User> Data { get; set; }
    }
}