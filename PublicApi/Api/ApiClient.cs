using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;
using PublicApi.Api;

namespace PublicAPI.Api
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
                var response = await _httpClient.GetAsync("Users");
                if (response.IsSuccessStatusCode)
                {
                    var users = await JsonSerializer.DeserializeAsync<List<User>>(await response.Content.ReadAsStreamAsync());
                    result.Value = users ?? new List<User>(); // Ensure non-null assignment  
                }
                else
                {
                    ((Result)result).AddError("_", "Failed to fetch Users");
                }
            }
            catch (Exception ex)
            {
                ((Result)result).AddError("_", ex.Message);
            }

            return result;
        }

        public async Task<Result<User>> Save(User user)
        {
            HttpResponseMessage response;
            Result<User> result = new Result<User>();

            if (user.Id == 0)
            {
                var content = new StringContent(JsonSerializer.Serialize(user), System.Text.Encoding.UTF8, "application/json");
                response = await _httpClient.PostAsync("Products", content);
            }
            else
            {
                var content = new StringContent(JsonSerializer.Serialize(user), System.Text.Encoding.UTF8, "application/json");
                response = await _httpClient.PutAsync("Products/" + user.Id, content);
            }

            if (response.IsSuccessStatusCode)
            {
                var userResult = await JsonSerializer.DeserializeAsync<User>(await response.Content.ReadAsStreamAsync());
                result.Value = userResult ?? new User(); // Ensure non-null assignment    
            }
            else
            {
                var errorResult = await JsonSerializer.DeserializeAsync<Result>(await response.Content.ReadAsStreamAsync());
                if (errorResult != null)
                {
                    // Copy errors from errorResult to result    
                    foreach (var kvp in errorResult.Error)
                    {
                        result.AddError(kvp.Key, msg);
                    }
                }
                else
                {
                    ((Result)result).AddError("_", "Unknown error");
                }
            }

            return result;
        }

        public async Task<Result> Delete(int id)
        {
            var response = await _httpClient.DeleteAsync("Users/" + id);
            var result = new Result();

            if (!response.IsSuccessStatusCode)
            {
                var errorResult = await JsonSerializer.DeserializeAsync<Result>(await response.Content.ReadAsStreamAsync());
                if (errorResult != null)
                {
                    foreach (var error in errorResult.Error) // Corrected loop to iterate over Error property as a string
                    {
                        result.AddError("_", error.ToString()); // Fixed to add error messages as strings
                    }
                }
                else
                {
                    result.AddError("_", "Unknown error");
                }
            }

            return result;
        }

        public async Task<Result<User>> Get(int id)
        {
            var result = new Result<User>();

            try
            {
                var response = await _httpClient.GetAsync("Products/" + id);
                if (response.IsSuccessStatusCode)
                {
                    var user = await JsonSerializer.DeserializeAsync<User>(await response.Content.ReadAsStreamAsync());
                    result.Value = user ?? new User(); // Ensure non-null assignment    
                }
                else
                {
                    ((Result)result).AddError("_", "Failed to fetch user"); // Explicitly cast to base class Result to resolve ambiguity  
                }
            }
            catch (Exception ex)
            {
                ((Result)result).AddError("_", ex.Message); // Explicitly cast to base class Result to resolve ambiguity  
            }

            return result;
        }

        Task<Result<bool>> IApiClient.Save(User user)
        {
            throw new NotImplementedException();
        }

        Task<Result<bool>> IApiClient.Delete(int id)
        {
            throw new NotImplementedException();
        }

        public Task<Result<List<T>>> List<T>()
        {
            throw new NotImplementedException();
        }
    }
}