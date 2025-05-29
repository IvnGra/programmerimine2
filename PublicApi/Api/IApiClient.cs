using System.Collections.Generic;
using System.Threading.Tasks;

namespace PublicApi.Api
{
    public interface IApiClient
    {
        // Generic list of T (used for reusability)
        Task<Result<List<T>>> List<T>();

        // Non-generic list of User for convenience (optional, if needed)
        Task<Result<List<User>>> List();

        // Get a specific T by ID (generic)
        Task<Result<T>> Get<T>(int id);

        // Save a specific T (generic)
        Task<Result<bool>> Save<T>(T item);

        // Delete by ID
        Task<Result<bool>> Delete(int id);
        Task<Result<bool>> Save(User user);
    }
}
