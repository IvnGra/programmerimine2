using KooliProjekt.Data;
using KooliProjekt.Services;
using KooliProjekt.Search;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using Xunit;

namespace KooliProjekt.UnitTests.ServiceTests
{
    public class MatchServiceTests : ServiceTestBase
    {
        private readonly IMatchesService _matchesService;

        public MatchServiceTests()
        {
            _matchesService = new MatchService(DbContext);
        }

        [Fact]
        public async Task Create_ShouldAddMatch()
        {
            // Arrange
            var match = new Match
            {
                Description = "Jalgpalli turniir",
                Team1_goals = 2,
                Team2_goals = 1,
                Name = "Football Match"
            };

            // Act
            await _matchesService.Create(match);

            // Assert
            var result = await _matchesService.Get(match.Id);
            Assert.NotNull(result);
            Assert.Equal(match.Team1, result.Team1);
            Assert.Equal(match.Team2, result.Team2);
            Assert.Equal(match.Team1_goals, result.Team1_goals);
            Assert.Equal(match.Team2_goals, result.Team2_goals);
        }

        [Fact]
        public async Task List_ShouldReturnPagedMatches()
        {
            // Arrange
            var team1 = new Team { TeamName = "teamname 1", TeamDescription = "algajad" };
            var team2 = new Team { TeamName = "teamname 2", TeamDescription = "proffid" };
            var match1 = new Match { Team1 = team1, Team2 = team2, Name = "Legion", Description = "Jalgpalli turniir"};
            var match2 = new Match { Team1 = team2, Team2 = team1, Name = "Friendly", Description = "Jalgapalli turniir"};

            await _matchesService.Create(match1);
            await _matchesService.Create(match2);

            // Act: Search for team names
            var search = new MatchesSearch { Keyword = "turniir" };
            var result = await _matchesService.List(1, 10, search);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(2, result.Results.Count);  // Now passes if List() searches Team.TeamName
        }

        [Fact]
        public async Task Get_ShouldReturnMatchById()
        {
            // Arrange
            var team1 = new Team { TeamName = "Team Alpha" ,TeamDescription = "algajad" };
            var team2 = new Team { TeamName = "Team Beta", TeamDescription = "proffid"};
            var match = new Match { Team1 = team1, Team2 = team2, Team1_goals = 2, Team2_goals = 1, Name = "FCkalju", Description = "Jalgpalli turniir" };
            await _matchesService.Create(match);

            // Act
            var result = await _matchesService.Get(match.Id);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(match.Id, result.Id);
            Assert.Equal(match.Team1, result.Team1);
            Assert.Equal(match.Team2, result.Team2);
            Assert.Equal(2, result.Team1_goals);
            Assert.Equal(1, result.Team2_goals);
        }

        [Fact]
        public async Task Save_ShouldUpdateMatch()
        {
            // Arrange
            var team1 = new Team { TeamName = "Team Alpha", TeamDescription = "algajad" };
            var team2 = new Team { TeamName = "Team Beta" , TeamDescription = "proffid" };
            var match = new Match { Team1 = team1, Team2 = team2, Team1_goals = 2, Team2_goals = 1, Name = "Legion", Description = "Jalgpalli turniir" };
            await _matchesService.Create(match);

            // Act
            match.Team1_goals = 3;
            match.Team2_goals = 2;
            await _matchesService.Save(match);

            // Assert
            var updatedMatch = await _matchesService.Get(match.Id);
            Assert.NotNull(updatedMatch);
            Assert.Equal(3, updatedMatch.Team1_goals);
            Assert.Equal(2, updatedMatch.Team2_goals);
        }

        [Fact]
        public async Task Delete_ShouldRemoveMatch()
        {
            // Arrange
            var team1 = new Team { TeamName = "Team Alpha", TeamDescription = "algajad" };
            var team2 = new Team { TeamName = "Team Beta" , TeamDescription = "proffid"};
            var match = new Match { Team1 = team1, Team2 = team2, Team1_goals = 2, Team2_goals = 1, Name = "FCkalju", Description = "Jalgpalli turniir" };
            await _matchesService.Create(match);

            // Act
            await _matchesService.Delete(match.Id);

            // Assert
            var result = await _matchesService.Get(match.Id);
            Assert.Null(result);
        }
    }
}
