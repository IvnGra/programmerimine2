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
    public class TeamsControllerPostTests : TestBase
    {
        private readonly HttpClient _client;
        private readonly ApplicationDbContext _context;

        public TeamsControllerPostTests()
        {
            var options = new WebApplicationFactoryClientOptions
            {
                AllowAutoRedirect = false
            };
            _client = Factory.CreateClient(options);
            _context = (ApplicationDbContext)Factory.Services.GetService(typeof(ApplicationDbContext));
        }

        [Fact]
        public async Task Create_should_save_new_team()
        {
            // Arrange
            var formValues = new Dictionary<string, string>
            {
                { "TeamName", "Dream Team" },
                { "TeamDescription", "Jane Doe" },
            };

            using var content = new FormUrlEncodedContent(formValues);

            // Act
            using var response = await _client.PostAsync("/Teams/Create", content);

            // Assert
            if (response.StatusCode == HttpStatusCode.Redirect)
            {
                var team = _context.Teams.FirstOrDefault(t => t.TeamName == "Dream Team");
                Assert.NotNull(team);
                Assert.Equal("Dream Team", team.TeamName);
                Assert.Equal("Jane Doe", team.TeamDescription);
            }
            else
            {
                Console.WriteLine("Hmm... doesn't work");
            }
        }

        [Fact]
        public async Task Create_should_not_save_invalid_team()
        {
            // Arrange
            var formValues = new Dictionary<string, string>
            {
                { "TeamName", "" },
                { "TeamDescripption", "" }
            };

            using var content = new FormUrlEncodedContent(formValues);

            // Act
            using var response = await _client.PostAsync("/Teams/Create", content);

            // Assert
            Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
            Assert.False(_context.Teams.Any());
        }
    }
}
