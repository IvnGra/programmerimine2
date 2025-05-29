using System.Collections.Generic;
using System.Threading.Tasks;

namespace WpfApp1.Api
{
    public interface IApiClient
    {
        Task<Result<User>> Save(User user);
        Task<Result<object>> Delete(int id);
        Task<Result<User>> Get(int id);
        Task<Result<List<T>>> List<T>();
    }
}
