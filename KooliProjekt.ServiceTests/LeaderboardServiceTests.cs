using KooliProjekt.Data;
using KooliProjekt.Data.Repositories;
using KooliProjekt.Services;
using Moq;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xunit;

namespace KooliProjekt.UnitTests.ServiceTests
{
    public class LeaderboardServiceTests
    {
        private readonly Mock<IUnitOfWork> _uowMock;
        private readonly Mock<ILeaderboardRepository> _repositoryMock;
        private readonly LeaderboardService _leaderboardService;

        public LeaderboardServiceTests()
        {
            _uowMock = new Mock<IUnitOfWork>();
            _repositoryMock = new Mock<ILeaderboardRepository>();
            _leaderboardService = new LeaderboardService(_uowMock.Object);

            _uowMock.SetupGet(u => u.LeaderboardReposiory)
                    .Returns(_repositoryMock.Object);
        }

        [Fact]
        public async Task List_Should_Return_List_Of_Leaderboards()
        {
            // Arrange
            var results = new List<Leaderboard>
            {
                new Leaderboard { Id = 1, Name = "Board 1" },
                new Leaderboard { Id = 2, Name = "Board 2" }
            };
            var pagedResult = new PagedResult<Leaderboard> { Results = results };
            _repositoryMock.Setup(r => r.List(It.IsAny<int>(), It.IsAny<int>()))
                           .ReturnsAsync(pagedResult);

            // Act
            var result = await _leaderboardService.List(1, 10);

            // Assert
            Assert.Equal(pagedResult, result);
        }

        [Fact]
        public async Task Get_Should_Return_Leaderboard()
        {
            // Arrange
            var board = new Leaderboard { Id = 1, Name = "Board 1" };
            _repositoryMock.Setup(r => r.Get(1)).ReturnsAsync(board);

            // Act
            var result = await _leaderboardService.Get(1);

            // Assert
            Assert.Equal(board, result);
        }

        [Fact]
        public async Task Save_Should_Call_Repository_Save()
        {
            // Arrange
            var board = new Leaderboard { Id = 1, Name = "Board 1" };

            // Act
            await _leaderboardService.Save(board);

            // Assert
            _repositoryMock.Verify(r => r.Save(board), Times.Once);
        }

        [Fact]
        public async Task Delete_Should_Call_Repository_Delete()
        {
            // Arrange
            int id = 1;

            // Act
            await _leaderboardService.Delete(id);

            // Assert
            _repositoryMock.Verify(r => r.Delete(id), Times.Once);
        }
    }
}
