using KooliProjekt.Data;
using KooliProjekt.Data.Repositories;
using KooliProjekt.Services;
using Moq;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xunit;

namespace KooliProjekt.UnitTests.ServiceTests
{
    public class TeamServiceTests
    {
        private readonly Mock<IUnitOfWork> _uowMock;
        private readonly Mock<ITeamRepository> _repositoryMock;
        private readonly TeamService _teamService;

        public TeamServiceTests()
        {
            _uowMock = new Mock<IUnitOfWork>();
            _repositoryMock = new Mock<ITeamRepository>();
            _teamService = new TeamService(_uowMock.Object);

            _uowMock.SetupGet(u => u.TeamRepository)
                    .Returns(_repositoryMock.Object);
        }

        [Fact]
        public async Task List_Should_Return_List_Of_Teams()
        {
            // Arrange
            var teams = new List<Team>
            {
                new Team { Id = 1, TeamName = "Team A" },
                new Team { Id = 2, TeamName = "Team B" }
            };
            var pagedResult = new PagedResult<Team> { Results = teams };
            _repositoryMock.Setup(r => r.List(It.IsAny<int>(), It.IsAny<int>()))
                           .ReturnsAsync(pagedResult);

            // Act
            var result = await _teamService.List(1, 10);

            // Assert
            Assert.Equal(pagedResult, result);
        }

        [Fact]
        public async Task Get_Should_Return_Team()
        {
            // Arrange
            var team = new Team { Id = 1, TeamName = "Team A" };
            _repositoryMock.Setup(r => r.Get(1)).ReturnsAsync(team);

            // Act
            var result = await _teamService.Get(1);

            // Assert
            Assert.Equal(team, result);
        }

        [Fact]
        public async Task Save_Should_Call_Repository_Save()
        {
            // Arrange
            var team = new Team { Id = 1, TeamName = "Team A" };

            // Act
            await _teamService.Save(team);

            // Assert
            _repositoryMock.Verify(r => r.Save(team), Times.Once);
        }

        [Fact]
        public async Task Delete_Should_Call_Repository_Delete()
        {
            // Arrange
            int id = 1;

            // Act
            await _teamService.Delete(id);

            // Assert
            _repositoryMock.Verify(r => r.Delete(id), Times.Once);
        }
    }
}
