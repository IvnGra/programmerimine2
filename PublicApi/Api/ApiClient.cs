using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;

namespace PublicApi.Api
{
    public class ApiClient : IApiClient
    {
        private readonly HttpClient _httpClient;

        public ApiClient()
        {
            _httpClient = new HttpClient
            {
                BaseAddress = new Uri("https://localhost:7136/api/")
            };
        }

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
                var users = await _httpClient.GetFromJsonAsync<List<User>>("Users");
                result.Value = users ?? new List<User>();
            }
            catch (Exception ex)
            {
                result.AddError("_", ex.Message);
            }
            return result;
        }

        public async Task<Result<User>> Save(User user)
        {
            var result = new Result<User>();
            try
            {
                HttpResponseMessage response;
                if (user.Id == 0)
                    response = await _httpClient.PostAsJsonAsync("Users", user);
                else
                    response = await _httpClient.PutAsJsonAsync($"Users/{user.Id}", user);

                if (response.IsSuccessStatusCode)
                {
                    var savedUser = await response.Content.ReadFromJsonAsync<User>();
                    result.Value = savedUser ?? new User();
                }
                else
                {
                    var errorResult = await response.Content.ReadFromJsonAsync<Result>();
                    if (errorResult != null)
                    {
                        foreach (var kvp in errorResult.Errors)
                            foreach (var msg in kvp.Value)
                                result.AddError(kvp.Key, msg);
                    }
                    else
                    {
                        result.AddError("_", $"HTTP {(int)response.StatusCode}: {response.ReasonPhrase}");
                    }
                }
            }
            catch (Exception ex)
            {
                result.AddError("_", ex.Message);
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
                    var errorResult = await response.Content.ReadFromJsonAsync<Result>();
                    if (errorResult != null)
                    {
                        foreach (var kvp in errorResult.Errors)
                            foreach (var msg in kvp.Value)
                                result.AddError(kvp.Key, msg);
                    }
                    else
                    {
                        result.AddError("_", $"HTTP {(int)response.StatusCode}: {response.ReasonPhrase}");
                    }
                }
            }
            catch (Exception ex)
            {
                result.AddError("_", ex.Message);
            }
            return result;
        }

        public async Task<Result<User>> Get(int id)
        {
            var result = new Result<User>();
            try
            {
                var user = await _httpClient.GetFromJsonAsync<User>($"Users/{id}");
                result.Value = user ?? new User();
            }
            catch (Exception ex)
            {
                result.AddError("_", ex.Message);
            }
            return result;
        }
    }
}
