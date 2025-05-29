using System.Collections.Generic;
using System.Threading.Tasks;

namespace PublicApi.Api
{
    public interface IApiClient
    {
        Task<Result<List<User>>> List();
        Task<Result<bool>> Save(User user);
        Task<Result<bool>> Delete(int id);
        Task<Result<List<T>>> List<T>();
    }
}
