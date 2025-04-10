using System;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using KooliProjekt.Data;
using KooliProjekt.IntegrationTests.Helpers;
using Xunit;
using static NuGet.Packaging.PackagingConstants;

namespace KooliProjekt.IntegrationTests
{
    [Collection("Sequential")]
    public class TournamentControllerGetTests : TestBase
    {
        private readonly HttpClient _client;
        private readonly ApplicationDbContext _context;

        public TournamentControllerGetTests()
        {
            _client = Factory.CreateClient();
            _context = (ApplicationDbContext)Factory.Services.GetService(typeof(ApplicationDbContext));
        }

        [Fact]
        public async Task Index_should_return_correct_response()
        {
            // Arrange

            // Act
            using var response = await _client.GetAsync("/Tournaments");

            // Assert
            response.EnsureSuccessStatusCode();
        }

        [Fact]
        public async Task Details_should_return_notfound_when_list_was_not_found()
        {
            // Arrange

            // Act
            using var response = await _client.GetAsync("/Tournaments/Details/100");

            // Assert
            Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
        }

        [Fact]
        public async Task Details_should_return_notfound_when_id_is_missing()
        {
            // Arrange

            // Act
            using var response = await _client.GetAsync("/Tournaments/Details/");

            // Assert
            Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
        }

        [Fact]
        public async Task Details_should_return_ok_when_list_was_found()
        {
            // Arrange
            var list = new Tournament{  TournamentName= "Test", Location = "Test" };
            _context.Tournaments.Add(list);
            _context.SaveChanges();

            // Act
            using var response = await _client.GetAsync("/Tournaments/Details/" + list.Id);

            // Assert
            response.EnsureSuccessStatusCode();
        }
    }
}