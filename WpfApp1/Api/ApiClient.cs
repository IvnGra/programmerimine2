using System;
using System.Collections.Generic;
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
            catch (HttpRequestException)
            {
                result.Error = "Cannot connect to server. Please try again later.";
            }
            catch (Exception ex)
            {
                result.Error = ex.Message;
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
                {
                    response = await _httpClient.PostAsJsonAsync("Users", user);
                }
                else
                {
                    response = await _httpClient.PutAsJsonAsync("Users/" + user.Id, user);
                }

                if (response.IsSuccessStatusCode)
                {
                    result.Value = user;
                }
                else
                {
                    result.Error = $"Server returned error: {response.StatusCode} - {response.ReasonPhrase}";
                }
            }
            catch (HttpRequestException)
            {
                result.Error = "Cannot connect to server. Please try again later.";
            }
            catch (Exception ex)
            {
                result.Error = ex.Message;
            }
            return result;
        }

        public async Task<Result<object>> Delete(int id)
        {
            var result = new Result<object>();
            try
            {
                var response = await _httpClient.DeleteAsync("Users/" + id);

                if (response.IsSuccessStatusCode)
                {
                    result.Value = null;
                }
                else
                {
                    result.Error = $"Server returned error: {response.StatusCode} - {response.ReasonPhrase}";
                    result.Value = null;
                }
            }
            catch (HttpRequestException)
            {
                result.Error = "Cannot connect to server. Please try again later.";
                result.Value = null;
            }
            catch (Exception ex)
            {
                result.Error = ex.Message;
                result.Value = null;
            }
            return result;
        }

        public async Task<Result<User>> Get(int id)
        {
            var result = new Result<User>();
            try
            {
                result.Value = await _httpClient.GetFromJsonAsync<User>($"Users/{id}");
            }
            catch (HttpRequestException)
            {
                result.Error = "Cannot connect to server. Please try again later.";
            }
            catch (Exception ex)
            {
                result.Error = ex.Message;
            }
            return result;
        }

        public async Task<Result<List<T>>> List<T>()
        {
            var result = new Result<List<T>>();
            try
            {
                var endpoint = typeof(T).Name + "s"; // Assumes plural form by adding 's'
                result.Value = await _httpClient.GetFromJsonAsync<List<T>>(endpoint);
            }
            catch (HttpRequestException)
            {
                result.Error = "Cannot connect to server. Please try again later.";
            }
            catch (Exception ex)
            {
                result.Error = ex.Message;
            }
            return result;
        }
    }
}
