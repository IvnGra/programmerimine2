using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace PublicApi.Api
{

    public class ApiClient : IApiClient
    {
        private readonly HttpClient _httpClient;

        public ApiClient(HttpClient httpClient)
        {
            _httpClient = httpClient ?? throw new ArgumentNullException(nameof(httpClient));
            if (_httpClient.BaseAddress == null)
                _httpClient.BaseAddress = new Uri("https://localhost:7136/api/");
        }

        public async Task<Result<List<User>>> List()
        {
            var result = new Result<List<User>>();

            try
            {
                var response = await _httpClient.GetAsync("Users");
                if (response.IsSuccessStatusCode)
                {
                    var users = await JsonSerializer.DeserializeAsync<List<User>>(await response.Content.ReadAsStreamAsync());
                    result.Value = users ?? new List<User>();
                }
                else
                {
                    result.AddError("HttpError", $"Failed to fetch users. Status code: {response.StatusCode}");
                }
            }
            catch (Exception ex)
            {
                result.AddError("Exception", ex.Message);
            }

            return result;
        }

        public async Task<Result<User>> Get(int id)
        {
            var result = new Result<User>();

            try
            {
                var response = await _httpClient.GetAsync($"Users/{id}");
                if (response.IsSuccessStatusCode)
                {
                    var user = await JsonSerializer.DeserializeAsync<User>(await response.Content.ReadAsStreamAsync());
                    result.Value = user ?? new User();
                }
                else
                {
                    result.AddError("HttpError", $"Failed to fetch user {id}. Status code: {response.StatusCode}");
                }
            }
            catch (Exception ex)
            {
                result.AddError("Exception", ex.Message);
            }

            return result;
        }

        public async Task<Result<User>> Save(User user)
        {
            var result = new Result<User>();
            HttpResponseMessage response;
            var content = new StringContent(JsonSerializer.Serialize(user), Encoding.UTF8, "application/json");

            try
            {
                if (user.Id == 0)
                    response = await _httpClient.PostAsync("Users", content);
                else
                    response = await _httpClient.PutAsync($"Users/{user.Id}", content);

                if (response.IsSuccessStatusCode)
                {
                    var userResult = await JsonSerializer.DeserializeAsync<User>(await response.Content.ReadAsStreamAsync());
                    result.Value = userResult ?? new User();
                }
                else
                {
                    result.AddError("HttpError", $"Save failed with status code {response.StatusCode}");
                    result.Value = user;
                }
            }
            catch (Exception ex)
            {
                result.AddError("Exception", ex.Message);
                result.Value = user;
            }

            return result;
        }

        public async Task<Result> Delete(int id)
        {
            var result = new Result();

            try
            {
                var response = await _httpClient.DeleteAsync($"Users/{id}");
                if (!response.IsSuccessStatusCode)
                {
                    result.AddError("HttpError", $"Delete failed with status code {response.StatusCode}");
                }
            }
            catch (Exception ex)
            {
                result.AddError("Exception", ex.Message);
            }

            return result;
        }
    }
}
