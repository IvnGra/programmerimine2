using System.Collections.Generic;
using System.Threading.Tasks;

namespace PublicApi.Api
{
    public interface IApiClient
    {
        Task<Result<List<User>>> List();
        Task<Result<User>> Get(int id);
        Task<Result<User>> Save(User user);
        Task<Result> Delete(int id);
    }
}
