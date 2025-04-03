using KooliProjekt.Data;
using KooliProjekt.Services;
using KooliProjekt.Search;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using Xunit;

namespace KooliProjekt.UnitTests.ServiceTests
{
    public class TeamServiceTests : ServiceTestBase
    {
        private readonly ITeamsService _teamService;

        public TeamServiceTests()
        {
            _teamService = new TeamService(DbContext);
        }

        [Fact]
        public async Task Create_ShouldAddTeam()
        {
            // Arrange
            var team = new Team
            {
                TeamName = "Team Alpha",
                TeamDescription = "algajad"
            };

            // Act
            await _teamService.Create(team);

            // Assert
            var result = await _teamService.Get(team.Id);
            Assert.NotNull(result);
            Assert.Equal(team.TeamName, result.TeamName);
        }

        [Fact]
        public async Task List_ShouldReturnPagedTeams()
        {
            // Arrange
            var team1 = new Team { TeamName = "Team Alpha", TeamDescription = "algajad" };
            var team2 = new Team { TeamName = "Team Beta", TeamDescription = "Proffid" };
            await _teamService.Create(team1);
            await _teamService.Create(team2);

            var search = new TeamsSearch { Keyword = "Team" };

            // Act
            var result = await _teamService.List(1, 10, search);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(2, result.Results.Count);
        }

        [Fact]
        public async Task Get_ShouldReturnTeamById()
        {
            // Arrange
            var team = new Team { TeamName = "Team Alpha", TeamDescription = "algajad" };
            await _teamService.Create(team);

            // Act
            var result = await _teamService.Get(team.Id);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(team.Id, result.Id);
            Assert.Equal("Team Alpha", result.TeamName);
        }

        [Fact]
        public async Task Save_ShouldUpdateTeam()
        {
            // Arrange
            var team = new Team
            {
                TeamName = "Team Alpha",
                TeamDescription = "algajad"

            };
            await _teamService.Create(team);

            // Act
            team.TeamName = "Team Gamma";
            await _teamService.Save(team);

            // Assert
            var updatedTeam = await _teamService.Get(team.Id);
            Assert.NotNull(updatedTeam);
            Assert.Equal("Team Gamma", updatedTeam.TeamName);
        }

        [Fact]
        public async Task Delete_ShouldRemoveTeam()
        {
            // Arrange
            var team = new Team { TeamName = "Team Alpha", TeamDescription = "Proffid"};
            await _teamService.Create(team);

            // Act
            await _teamService.Delete(team.Id);

            // Assert
            var result = await _teamService.Get(team.Id);
            Assert.Null(result);
        }
    }
}
