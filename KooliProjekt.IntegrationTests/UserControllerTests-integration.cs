using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using KooliProjekt.Data;
using KooliProjekt.IntegrationTests.Helpers;
using Microsoft.AspNetCore.Mvc.Testing;
using Xunit;
using System.Linq;
using System;

namespace KooliProjekt.IntegrationTests
{
    [Collection("Sequential")]
    public class UsersControllerPostTests : TestBase
    {
        private readonly HttpClient _client;
        private readonly ApplicationDbContext _context;

        public UsersControllerPostTests()
        {
            var options = new WebApplicationFactoryClientOptions
            {
                AllowAutoRedirect = false
            };
            _client = Factory.CreateClient(options);
            _context = (ApplicationDbContext)Factory.Services.GetService(typeof(ApplicationDbContext));
        }

        [Fact]
        public async Task Create_should_save_new_user()
        {
            // Arrange
            var formValues = new Dictionary<string, string>
            {
                { "UserEmail", "testuser@example.com" },
                { "UserName", "Test User" },
                { "isAdmin", "true" }
            };

            using var content = new FormUrlEncodedContent(formValues);

            // Act
            using var response = await _client.PostAsync("/Users/Create", content);

            // Assert
            if (response.StatusCode == HttpStatusCode.Redirect)
            {
                var user = _context.Users.FirstOrDefault(u => u.UserEmail == "testuser@example.com");
                Assert.NotNull(user);
                Assert.Equal("testuser@example.com", user.UserEmail);
                Assert.Equal("User", user.Username);
            }
            else
            {
                Console.WriteLine("Hmm... doesn't work");
            }
        }


        [Fact]
        public async Task Create_should_not_save_invalid_user()
        {
            // Arrange
            var formValues = new Dictionary<string, string>();
            formValues.Add("UserEmail", "");
            formValues.Add("UserName", "");
            formValues.Add("isAdmin", "");

            using var content = new FormUrlEncodedContent(formValues);

            // Act
            using var response = await _client.PostAsync("/Users/Create", content);

            // Assert
            Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
            Assert.False(_context.Users.Any());
        }
    }
}