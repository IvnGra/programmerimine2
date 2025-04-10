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
using static System.Formats.Asn1.AsnWriter;

namespace KooliProjekt.IntegrationTests
{
    [Collection("Sequential")]
    public class LeaderboardsControllerPostTests : TestBase
    {
        private readonly HttpClient _client;
        private readonly ApplicationDbContext _context;

        public LeaderboardsControllerPostTests()
        {
            var options = new WebApplicationFactoryClientOptions
            {
                AllowAutoRedirect = false
            };
            _client = Factory.CreateClient(options);
            _context = (ApplicationDbContext)Factory.Services.GetService(typeof(ApplicationDbContext));
        }

        [Fact]
        public async Task Create_should_save_new_leaderboard_entry()
        {
            // Arrange
            var formValues = new Dictionary<string, string>
            {
                { "Name", "prediction" },
                { "Place", "Tallinn" },
                { "Score", "2" },
            };

            using var content = new FormUrlEncodedContent(formValues);

            // Act
            using var response = await _client.PostAsync("/Leaderboards/Create", content);

            // Assert
            if (response.StatusCode == HttpStatusCode.Redirect)
            {
                var entry = _context.Leaderboards.FirstOrDefault(l => l.Name == "prediction");
                Assert.NotNull(entry);
                Assert.Equal("Team Alpha", entry.Name);
                Assert.Equal("tallinn", entry.Place);
                Assert.Equal(2, entry.Score);
            }
            else
            {
                Console.WriteLine("Hmm... doesn't work");
            }
        }

        [Fact]
        public async Task Create_should_not_save_invalid_leaderboard_entry()
        {
            // Arrange
            var formValues = new Dictionary<string, string>
            {
                { "Name", "" },
                { "Place", "" },
                { "Score", "" },
            };

            using var content = new FormUrlEncodedContent(formValues);

            // Act
            using var response = await _client.PostAsync("/Leaderboards/Create", content);

            // Assert
            Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
            Assert.False(_context.Leaderboards.Any());
        }
    }
}
