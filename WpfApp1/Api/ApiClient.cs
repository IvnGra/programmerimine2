﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Runtime.ConstrainedExecution;
using System.Text;
using System.Threading.Tasks;

namespace WpfApp1.Api
{
    class ApiClient : IApiClient
    {
        private readonly HttpClient _httpClient;
        public ApiClient()
        {
            _httpClient = new HttpClient();
            _httpClient.BaseAddress = new Uri("https://localhost:7136/api/");
        }
        public async Task<List<User>> List()
        {
            var result = await _httpClient.GetFromJsonAsync<List<User>>("Users");

            return result;
        }
        public async Task Save(User list)
        {
            if (list.Id == 0)
            {
                await _httpClient.PostAsJsonAsync("Users", list);
            }
            else
            {
                await _httpClient.PutAsJsonAsync("Users/" + list.Id, list);
            }
        }
        public async Task Delete(int id)
        {
            await _httpClient.DeleteAsync("Users/" + id);
        }
        public Task<IEnumerable<object>> List<T>()
        {
            throw new NotImplementedException();
        }
    }
}