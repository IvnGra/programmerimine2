using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using KooliProjekt.Data;
using KooliProjekt.Models;
using KooliProjekt.IntegrationTests.Helpers;
using Xunit;

namespace KooliProjekt.IntegrationTests
{
    [Collection("Sequential")]
    public class UsersControllerTests : TestBase
    {
        private readonly HttpClient _client;
        private readonly ApplicationDbContext _context;

        public UsersControllerTests()
        {
            _client = Factory.CreateClient();
            _context = (ApplicationDbContext)Factory.Services.GetService(typeof(ApplicationDbContext));
        }

        [Fact]
        public async Task Index_should_return_success_status_code()
        {
            // Arrange

            // Act
            using var response = await _client.GetAsync("/Users");

            // Assert
            response.EnsureSuccessStatusCode();
        }

        [Fact]
        public async Task Details_should_return_notfound_when_user_does_not_exist()
        {
            // Arrange

            // Act
            using var response = await _client.GetAsync("/Users/Details/100");

            // Assert
            Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
        }

        [Fact]
        public async Task Details_should_return_notfound_when_id_is_missing()
        {
            // Arrange

            // Act
            using var response = await _client.GetAsync("/Users/Details/");

            // Assert
            Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
        }

        [Fact]
        public async Task Details_should_return_success_status_code_when_user_exists()
        {
            // Arrange
            var user = new User { UserEmail = "testuser@example.com", Username = "Test User", IsAdmin = true };
            _context.Users.Add(user);
            await _context.SaveChangesAsync();

            // Act
            using var response = await _client.GetAsync($"/Users/Details/{user.Id}");

            // Assert
            response.EnsureSuccessStatusCode();
        }
    }
}