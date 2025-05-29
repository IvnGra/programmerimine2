using KooliProjekt.Data;
using KooliProjekt.Data.Repositories;
using KooliProjekt.Services;
using Moq;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xunit;

namespace KooliProjekt.UnitTests.ServiceTests
{
    public class TournamentServiceTests
    {
        private readonly Mock<IUnitOfWork> _uowMock;
        private readonly Mock<ITournamentRepository> _repositoryMock;
        private readonly TournamentService _tournamentService;

        public TournamentServiceTests()
        {
            _uowMock = new Mock<IUnitOfWork>();
            _repositoryMock = new Mock<ITournamentRepository>();
            _tournamentService = new TournamentService(_uowMock.Object);

            _uowMock.SetupGet(u => u.TournamentRepository)
                    .Returns(_repositoryMock.Object);
        }

        [Fact]
        public async Task List_Should_Return_List_Of_Tournaments()
        {
            // Arrange
            var tournaments = new List<Tournament>
            {
                new Tournament { Id = 1, TournamentName = "Champions Cup" },
                new Tournament { Id = 2, TournamentName = "World League" }
            };
            var pagedResult = new PagedResult<Tournament> { Results = tournaments };
            _repositoryMock.Setup(r => r.List(It.IsAny<int>(), It.IsAny<int>()))
                           .ReturnsAsync(pagedResult);

            // Act
            var result = await _tournamentService.List(1, 10);

            // Assert
            Assert.Equal(pagedResult, result);
        }

        [Fact]
        public async Task Get_Should_Return_Tournament()
        {
            // Arrange
            var tournament = new Tournament { Id = 1, TournamentName = "Champions Cup" };
            _repositoryMock.Setup(r => r.Get(1)).ReturnsAsync(tournament);

            // Act
            var result = await _tournamentService.Get(1);

            // Assert
            Assert.Equal(tournament, result);
        }

        [Fact]
        public async Task Save_Should_Call_Repository_Save()
        {
            // Arrange
            var tournament = new Tournament { Id = 1, TournamentName = "Champions Cup" };

            // Act
            await _tournamentService.Save(tournament);

            // Assert
            _repositoryMock.Verify(r => r.Save(tournament), Times.Once);
        }

        [Fact]
        public async Task Delete_Should_Call_Repository_Delete()
        {
            // Arrange
            int id = 1;

            // Act
            await _tournamentService.Delete(id);

            // Assert
            _repositoryMock.Verify(r => r.Delete(id), Times.Once);
        }
    }
}
