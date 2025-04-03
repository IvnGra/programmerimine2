using KooliProjekt.Data;
using KooliProjekt.Services;
using KooliProjekt.Search;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using Xunit;

namespace KooliProjekt.UnitTests.ServiceTests
{
    public class TournamentServiceTests : ServiceTestBase
    {
        private readonly ITournamentsService _tournamentService;

        public TournamentServiceTests()
        {
            _tournamentService = new TournamentService(DbContext);
        }

        [Fact]
        public async Task Create_ShouldAddTournament()
        {
            // Arrange
            var tournament = new Tournament
            {
                TournamentName = "Champions League",
                Location = "Germany" ,
                Year = 2022
            };

            // Act
            await _tournamentService.Create(tournament);

            // Assert
            var result = await _tournamentService.Get(tournament.Id);
            Assert.NotNull(result);
            Assert.Equal(tournament.TournamentName, result.TournamentName);
            Assert.Equal(tournament.Location, result.Location);
            Assert.Equal(tournament.Year, result.Year);
        }

        [Fact]
        public async Task List_ShouldReturnPagedTournaments()
        {
            // Arrange
            var tournament1 = new Tournament { TournamentName = "Champions League", Location = "Europe", Year = 2024 };
            var tournament2 = new Tournament { TournamentName = "Europa League", Location = "Europe", Year = 2024 };
            await _tournamentService.Create(tournament1);
            await _tournamentService.Create(tournament2);

            var search = new TournamentsSearch { Keyword = "League" };

            // Act
            var result = await _tournamentService.List(1, 10, search);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(2, result.Results.Count);
        }

        [Fact]
        public async Task Get_ShouldReturnTournamentById()
        {
            // Arrange
            var tournament = new Tournament { TournamentName = "Champions League", Location = "Europe", Year = 2024 };
            await _tournamentService.Create(tournament);

            // Act
            var result = await _tournamentService.Get(tournament.Id);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(tournament.Id, result.Id);
            Assert.Equal("Champions League", result.TournamentName);
            Assert.Equal("Europe", result.Location);
            Assert.Equal(2024, result.Year);
        }

        [Fact]
        public async Task Save_ShouldUpdateTournament()
        {
            // Arrange
            var tournament = new Tournament
            {
                TournamentName = "Champions League",
                Location = "Europe",
                Year = 2024
            };
            await _tournamentService.Create(tournament);

            // Act
            tournament.TournamentName = "World Cup";
            tournament.Location = "Global";
            await _tournamentService.Save(tournament);

            // Assert
            var updatedTournament = await _tournamentService.Get(tournament.Id);
            Assert.NotNull(updatedTournament);
            Assert.Equal("World Cup", updatedTournament.TournamentName);
            Assert.Equal("Global", updatedTournament.Location);
        }

        [Fact]
        public async Task Delete_ShouldRemoveTournament()
        {
            // Arrange
            var tournament = new Tournament { TournamentName = "Champions League", Location = "Europe", Year = 2024 };
            await _tournamentService.Create(tournament);

            // Act
            await _tournamentService.Delete(tournament.Id);

            // Assert
            var result = await _tournamentService.Get(tournament.Id);
            Assert.Null(result);
        }
    }
}
