using KooliProjekt.Data;
using KooliProjekt.Services;
using KooliProjekt.Search;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using Xunit;

namespace KooliProjekt.UnitTests.ServiceTests
{
    public class LeaderboardServiceTests : ServiceTestBase
    {
        private readonly ILeaderboardService _leaderboardService;

        public LeaderboardServiceTests()
        {
            _leaderboardService = new LeaderboardService(DbContext);
        }

        [Fact]
        public async Task Create_ShouldAddLeaderboard()
        {
            // Arrange
            var leaderboard = new Leaderboard
            {
                Name = "Test Player",
                Score = 1000,
                place = "Tartu mnt69",
            };

            // Act
            await _leaderboardService.Create(leaderboard);

            // Assert
            var result = await _leaderboardService.Get(leaderboard.Id);
            Assert.NotNull(result);
            Assert.Equal(leaderboard.Name, result.Name);
            Assert.Equal(leaderboard.Score, result.Score);
        }

        [Fact]
        public async Task List_ShouldReturnPagedLeaderboard()
        {
            // Arrange
            var Leaderboard1 = new Leaderboard { Name = "Player 1", Score = 1200 };
            var Leaderboard2 = new Leaderboard { Name = "Player 2", Score = 800 };
            await _leaderboardService.Create(Leaderboard1);
            await _leaderboardService.Create(Leaderboard2);

            var search = new LeaderboardsSearch { Keyword = "Leaderboard" };

            // Act
            var result = await _leaderboardService.List(1, 10, search);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(2, result.Results.Count);
        }

        [Fact]
        public async Task Get_ShouldReturnLeaderboardById()
        {
            // Arrange
            var leaderboard = new Leaderboard { Name = "Test Player", Score = 1000 };
            await _leaderboardService.Create(leaderboard);

            // Act
            var result = await _leaderboardService.Get(leaderboard.Id);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(leaderboard.Id, result.Id);
            Assert.Equal("Test Player", result.Name);
            Assert.Equal(1000, result.Score);
        }

        [Fact]
        public async Task Save_ShouldUpdateLeaderboard()
        {
            // Arrange
            var leaderboard = new Leaderboard 
            { Name = "Initial Player"
            , Score = 500 
            };

            await _leaderboardService.Create(leaderboard);

            // Act
            leaderboard.Name = "Updated Player";
            leaderboard.Score = 1500;
            await _leaderboardService.Save(leaderboard);

            // Assert
            var updatedleaderboard = await _leaderboardService.Get(leaderboard.Id);
            Assert.NotNull(updatedleaderboard);
            Assert.Equal("Updated Player", updatedleaderboard.Name);
            Assert.Equal(1500, updatedleaderboard.Score);
        }

        [Fact]
        public async Task Delete_ShouldRemoveLeaderboard()
        {
            // Arrange
            var leaderboard = new Leaderboard { Name = "Test Player", Score = 1000 };
            await _leaderboardService.Create(leaderboard);

            // Act
            await _leaderboardService.Delete(leaderboard.Id);

            // Assert
            var result = await _leaderboardService.Get(leaderboard.Id);
            Assert.Null(result);
        }
    }
}
