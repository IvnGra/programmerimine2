using KooliProjekt.Data;
using KooliProjekt.Data.Repositories;
using KooliProjekt.Services;
using Moq;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xunit;
using DataModels = KooliProjekt.Data; // ✅ Alias to resolve ambiguity

namespace KooliProjekt.UnitTests.ServiceTests
{
    public class MatchServiceTests
    {
        private readonly Mock<IUnitOfWork> _uowMock;
        private readonly Mock<IMatchRepository> _repositoryMock;
        private readonly MatchService _matchService;

        public MatchServiceTests()
        {
            _uowMock = new Mock<IUnitOfWork>();
            _repositoryMock = new Mock<IMatchRepository>();
            _matchService = new MatchService(_uowMock.Object);

            _uowMock.SetupGet(u => u.MatchRepository)
                    .Returns(_repositoryMock.Object);
        }

        [Fact]
        public async Task List_Should_Return_List_Of_Matches()
        {
            // Arrange
            var results = new List<DataModels.Match>
            {
                new DataModels.Match { Id = 1, Round = "Round 1", Team1_goals = 3, Team2_goals = 2 },
                new DataModels.Match { Id = 2, Round = "Round 2", Team1_goals = 1, Team2_goals = 1 }
            };
            var pagedResult = new PagedResult<DataModels.Match> { Results = results };
            _repositoryMock.Setup(r => r.List(It.IsAny<int>(), It.IsAny<int>()))
                           .ReturnsAsync(pagedResult);

            // Act
            var result = await _matchService.List(1, 10);

            // Assert
            Assert.Equal(pagedResult, result);
        }

        [Fact]
        public async Task Get_Should_Return_Match()
        {
            // Arrange
            var match = new DataModels.Match { Id = 1, Team1_goals = 3, Team2_goals = 2 };
            _repositoryMock.Setup(r => r.Get(1)).ReturnsAsync(match);

            // Act
            var result = await _matchService.Get(1);

            // Assert
            Assert.Equal(match, result);
        }

        [Fact]
        public async Task Save_Should_Call_Repository_Save()
        {
            // Arrange
            var match = new DataModels.Match { Id = 1, Team1_goals = 3, Team2_goals = 2 };

            // Act
            await _matchService.Save(match);

            // Assert
            _repositoryMock.Verify(r => r.Save(match), Times.Once);
        }

        [Fact]
        public async Task Delete_Should_Call_Repository_Delete()
        {
            // Arrange
            int id = 1;

            // Act
            await _matchService.Delete(id);

            // Assert
            _repositoryMock.Verify(r => r.Delete(id), Times.Once);
        }
    }
}
