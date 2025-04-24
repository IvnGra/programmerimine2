using KooliProjekt.WpfApp.Api;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;

namespace WpfApp1.Api
{
    public class ApiClient : IApiClient
    {
        private readonly HttpClient _httpClient;

        public ApiClient()
        {
            _httpClient = new HttpClient();
            _httpClient.BaseAddress = new Uri("https://localhost:7136/api/");
        }

        public async Task<Result<List<User>>> List()
        {
            var result = new Result<List<User>>();

            try
            {
                result.Value = await _httpClient.GetFromJsonAsync<List<User>>("Users");
            }
            catch (Exception ex)
            {
                result.Error = ex.Message;
            }

            return result;
        }
        public async Task<Result> Save(User user)
        {
            var result = new Result();

            try
            {
                if (user.Id == 0)
                {
                    await _httpClient.PostAsJsonAsync("Users", user);
                }
                else
                {
                    await _httpClient.PutAsJsonAsync("Users/" + user.Id, user);
                }
            }

            catch (Exception ex)
            {
                result.Error = ex.Message;
            }

            return result;
        }

        public async Task<Result> Delete(int id)
        {
            var result = new Result();

            try
            {
                await _httpClient.DeleteAsync("Users/" + id);
            }
            catch (Exception ex)
            {
                result.Error = ex.Message;
            }

            return result;
        }
    }
}