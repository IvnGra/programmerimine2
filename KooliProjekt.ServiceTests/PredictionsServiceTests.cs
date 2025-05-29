using KooliProjekt.Data;
using KooliProjekt.Data.Repositories;
using KooliProjekt.Services;
using Moq;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xunit;

namespace KooliProjekt.UnitTests.ServiceTests
{
    public class PredictionServiceTests
    {
        private readonly Mock<IUnitOfWork> _uowMock;
        private readonly Mock<IPredicitonRepository> _repositoryMock;
        private readonly PredictionsService _predictionService;

        public PredictionServiceTests()
        {
            _uowMock = new Mock<IUnitOfWork>();
            _repositoryMock = new Mock<IPredicitonRepository>();
            _predictionService = new PredictionsService(_uowMock.Object);

            _uowMock.SetupGet(u => u.PredictionRepository)
                    .Returns(_repositoryMock.Object);
        }

        [Fact]
        public async Task List_Should_Return_List_Of_Predictions()
        {
            // Arrange
            var predictions = new List<Prediction>
            {
                new Prediction { Id = 1, UserId = 101, MatchId = 201, Team1_predicted_goals = 2, Team2_predicted_goals = 1 },
                new Prediction { Id = 2, UserId = 102, MatchId = 202, Team1_predicted_goals = 0, Team2_predicted_goals = 3 }
            };
            var pagedResult = new PagedResult<Prediction> { Results = predictions };
            _repositoryMock.Setup(r => r.List(It.IsAny<int>(), It.IsAny<int>()))
                           .ReturnsAsync(pagedResult);

            // Act
            var result = await _predictionService.List(1, 10);

            // Assert
            Assert.Equal(pagedResult, result);
        }

        [Fact]
        public async Task Get_Should_Return_Prediction()
        {
            // Arrange
            var prediction = new Prediction { Id = 1, UserId = 101, MatchId = 201, Team1_predicted_goals = 1, Team2_predicted_goals = 2 };
            _repositoryMock.Setup(r => r.Get(1)).ReturnsAsync(prediction);

            // Act
            var result = await _predictionService.Get(1);

            // Assert
            Assert.Equal(prediction, result);
        }

        [Fact]
        public async Task Save_Should_Call_Repository_Save()
        {
            // Arrange
            var prediction = new Prediction { Id = 1, UserId = 101, MatchId = 201, Team1_predicted_goals = 2, Team2_predicted_goals = 2 };

            // Act
            await _predictionService.Save(prediction);

            // Assert
            _repositoryMock.Verify(r => r.Save(prediction), Times.Once);
        }

        [Fact]
        public async Task Delete_Should_Call_Repository_Delete()
        {
            // Arrange
            int id = 1;

            // Act
            await _predictionService.Delete(id);

            // Assert
            _repositoryMock.Verify(r => r.Delete(id), Times.Once);
        }
    }
}