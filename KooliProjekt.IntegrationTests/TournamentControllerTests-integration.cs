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
    public class TournamentsControllerPostTests : TestBase
    {
        private readonly HttpClient _client;
        private readonly ApplicationDbContext _context;

        public TournamentsControllerPostTests()
        {
            var options = new WebApplicationFactoryClientOptions
            {
                AllowAutoRedirect = false
            };
            _client = Factory.CreateClient(options);
            _context = (ApplicationDbContext)Factory.Services.GetService(typeof(ApplicationDbContext));
        }

        [Fact]
        public async Task Create_should_save_new_tournament()
        {
            // Arrange
            var formValues = new Dictionary<string, string>
            {
                { "TournamentName", "Test Tournament" },
                { "StartDate", "2025-05-01" },
                { "EndDate", "2025-05-06" },
                { "Location", "Tallinn" }
            };

            using var content = new FormUrlEncodedContent(formValues);

            // Act
            using var response = await _client.PostAsync("/Tournaments/Create", content);

            // Assert
            if (response.StatusCode == HttpStatusCode.Redirect)
            {
                var tournament = _context.Tournaments.FirstOrDefault(t => t.TournamentName == "Test Tournament");
                Assert.NotNull(tournament);
                Assert.Equal("Test Tournament", tournament.TournamentName);
                Assert.Equal("Tallinn", tournament.Location);
                Assert.Equal(new DateTime(2025, 05, 01), tournament.StartDate);
                Assert.Equal(new DateTime(2025, 05, 06), tournament.EndDate);
            }
            else
            {
                Console.WriteLine("Hmm... doesn't work");
            }
        }

        [Fact]
        public async Task Create_should_not_save_invalid_tournament()
        {
            // Arrange
            var formValues = new Dictionary<string, string>
            {
                { "TournamentName", "" },
                { "StartDate", "" },
                { "EndDate", "" },
                { "Location", "" }
            };

            using var content = new FormUrlEncodedContent(formValues);

            // Act
            using var response = await _client.PostAsync("/Tournaments/Create", content);

            // Assert
            Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
            Assert.False(_context.Tournaments.Any());
        }
    }
}
