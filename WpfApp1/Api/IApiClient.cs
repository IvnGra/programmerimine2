
using KooliProjekt.Data;

namespace WpfApp1.Api
{
    interface IApiClient
    {
        Task<List<User>> List();
        Task Save(User list);
        Task Delete(int id);
    }
}