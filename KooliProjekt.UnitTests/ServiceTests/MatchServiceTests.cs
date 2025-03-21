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
                Team1_name = "Team Alpha",
                Team2_name = "Team Beta",
                Team1_goals = 2,
                Team2_goals = 1,
                Name = "Football Match"
            };

            // Act
            await _matchesService.Create(match);

            // Assert
            var result = await _matchesService.Get(match.Id);
            Assert.NotNull(result);
            Assert.Equal(match.Team1_name, result.Team1_name);
            Assert.Equal(match.Team2_name, result.Team2_name);
            Assert.Equal(match.Team1_goals, result.Team1_goals);
            Assert.Equal(match.Team2_goals, result.Team2_goals);
        }

        [Fact]
        public async Task List_ShouldReturnPagedMatches()
        {
            // Arrange
            var match1 = new Match { Team1_name = "Team Alpha", Team2_name = "Team Beta", Team1_goals = 2, Team2_goals = 1 };
            var match2 = new Match { Team1_name = "Team Gamma", Team2_name = "Team Delta", Team1_goals = 3, Team2_goals = 2 };
            await _matchesService.Create(match1);
            await _matchesService.Create(match2);

            var search = new MatchesSearch { Keyword = "Match" };

            // Act
            var result = await _matchesService.List(1, 10, search);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(2, result.Results.Count);
        }

        [Fact]
        public async Task Get_ShouldReturnMatchById()
        {
            // Arrange
            var match = new Match { Team1_name = "Team Alpha", Team2_name = "Team Beta", Team1_goals = 2, Team2_goals = 1 };
            await _matchesService.Create(match);

            // Act
            var result = await _matchesService.Get(match.Id);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(match.Id, result.Id);
            Assert.Equal("Team Alpha", result.Team1_name);
            Assert.Equal("Team Beta", result.Team2_name);
            Assert.Equal(2, result.Team1_goals);
            Assert.Equal(1, result.Team2_goals);
        }

        [Fact]
        public async Task Save_ShouldUpdateMatch()
        {
            // Arrange
            var match = new Match
            {
                Team1_name = "Team Alpha",
                Team2_name = "Team Beta",
                Team1_goals = 2,
                Team2_goals = 1,
                Name = "Football Match"
            };
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
            var match = new Match { Team1_name = "Team Alpha", Team2_name = "Team Beta", Team1_goals = 2, Team2_goals = 1 };
            await _matchesService.Create(match);

            // Act
            await _matchesService.Delete(match.Id);

            // Assert
            var result = await _matchesService.Get(match.Id);
            Assert.Null(result);
        }
    }
}
