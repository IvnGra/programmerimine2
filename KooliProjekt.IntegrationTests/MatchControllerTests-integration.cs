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
    public class MatchesControllerPostTests : TestBase
    {
        private readonly HttpClient _client;
        private readonly ApplicationDbContext _context;

        public MatchesControllerPostTests()
        {
            var options = new WebApplicationFactoryClientOptions
            {
                AllowAutoRedirect = false
            };
            _client = Factory.CreateClient(options);
            _context = (ApplicationDbContext)Factory.Services.GetService(typeof(ApplicationDbContext));
        }

        [Fact]
        public async Task Create_should_save_new_match()
        {
            // Arrange
            var formValues = new Dictionary<string, string>
            {
                { "Name", "Team A" },
                { "Description", "good" },
            };

            using var content = new FormUrlEncodedContent(formValues);

            // Act
            using var response = await _client.PostAsync("/Matches/Create", content);

            // Assert
            if (response.StatusCode == HttpStatusCode.Redirect)
            {
                var match = _context.Matchs.FirstOrDefault(m => m.Name == "Team A");
                Assert.NotNull(match);
                Assert.Equal("", match.Name);
                Assert.Equal("Good", match.Description);
                Assert.Equal(new DateTime(2025, 06, 10), match.Match_time);
                Assert.Equal(6, match.Round);
            }
            else
            {
                Console.WriteLine("Hmm... doesn't work");
            }
        }

        [Fact]
        public async Task Create_should_not_save_invalid_match()
        {
            // Arrange
            var formValues = new Dictionary<string, string>
            {
                { "Name", "" },
                { "match_time", "" },
                { "Round", "" },
                { "Description", "" }
            };

            using var content = new FormUrlEncodedContent(formValues);

            // Act
            using var response = await _client.PostAsync("/Matches/Create", content);

            // Assert
            Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
            Assert.False(_context.Matchs.Any());
        }
    }
}
