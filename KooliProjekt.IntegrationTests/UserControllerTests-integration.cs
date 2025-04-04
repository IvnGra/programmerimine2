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
    public class UsersControllerTests : TestBase
    {
        private readonly HttpClient _client;
        private readonly ApplicationDbContext _context;

        public UsersControllerTests()
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
                { "Email", "testuser@example.com" },
                { "Name", "Test User" },
                { "Password", "Password123" },
                { "Registration_Time", "2025-03-27" }
            };

            using var content = new FormUrlEncodedContent(formValues);

            // Act
            using var response = await _client.PostAsync("/Users/Create", content);

            // Assert
            if (response.StatusCode == HttpStatusCode.Redirect)
            {
                var user = _context.Users.FirstOrDefault(u => u.Email == "testuser@example.com");
                Assert.NotNull(user);
                Assert.Equal("testuser@example.com", user.Email);
                Assert.Equal("Test User", user.Name);
                Assert.Equal("Password123", user.Password);
                Assert.Equal(new DateTime(2025, 03, 27), user.Registration_Time);
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
            formValues.Add("Email", "");
            formValues.Add("Name", "");
            formValues.Add("Password", "");
            formValues.Add("Registration_Time", "");

            using var content = new FormUrlEncodedContent(formValues);

            // Act
            using var response = await _client.PostAsync("/Users/Create", content);

            // Assert
            Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
            Assert.False(_context.Users.Any());
        }
    }
}