using KooliProjekt.Data;
using KooliProjekt.Services;
using KooliProjekt.Search;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using Xunit;

namespace KooliProjekt.UnitTests.ServiceTests
{
    public class PredictionServiceTests : ServiceTestBase
    {
        private readonly IPredictionService _predictionService;

        public PredictionServiceTests()
        {
            _predictionService = new PredictionsService(DbContext);
        }

        [Fact]
        public async Task Create_ShouldAddPrediction()
        {
            // Arrange
            var prediction = new Prediction
            {
                Name = "Football Match",
                Team1_predicted_goals = 4,
                Team2_predicted_goals = 6,
                Points = 10,
                PointsEarned = 2,
                Description = "cool"
            };

            // Act
            await _predictionService.Create(prediction);

            // Assert
            var result = await _predictionService.Get(prediction.Id);
            Assert.NotNull(result);
            Assert.Equal(prediction.User, result.User);
            Assert.Equal(prediction.Team1_predicted_goals, result.Team1_predicted_goals);
            Assert.Equal(prediction.Team2_predicted_goals, result.Team2_predicted_goals);
        }

        [Fact]
        public async Task List_ShouldReturnPagedPredictions()
        {
            // Arrange
            var prediction1 = new Prediction { Name = "Football Match1", Team1_predicted_goals = 2, Team2_predicted_goals = 1 };
            var prediction2 = new Prediction { Name = "Football Match2", Team1_predicted_goals = 3, Team2_predicted_goals = 2 };
            await _predictionService.Create(prediction1);
            await _predictionService.Create(prediction2);

            var search = new PredictionsSearch { Keyword = "Prediction" };

            // Act
            var result = await _predictionService.List(1, 10, search);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(2, result.Results.Count);
        }

        [Fact]
        public async Task Get_ShouldReturnPredictionById()
        {
            // Arrange
            var prediction = new Prediction { Name = "Football Match", Team1_predicted_goals = 2, Team2_predicted_goals = 1 };
            await _predictionService.Create(prediction);

            // Act
            var result = await _predictionService.Get(prediction.Id);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(prediction.Id, result.Id);
            Assert.Equal("Football Match", result.Name);
            Assert.Equal(2, result.Team1_predicted_goals);
            Assert.Equal(1, result.Team2_predicted_goals);
        }

        [Fact]
        public async Task Save_ShouldUpdatePrediction()
        {
            // Arrange
            var prediction = new Prediction
            {
                Name = "Football Match",
                Team1_predicted_goals = 2,
                Team2_predicted_goals = 1,
                Description = "cool"
            };
            await _predictionService.Create(prediction);

            // Act
            prediction.Team1_predicted_goals = 3;
            prediction.Team2_predicted_goals = 2;
            await _predictionService.Save(prediction);

            // Assert
            var updatedPrediction = await _predictionService.Get(prediction.Id);
            Assert.NotNull(updatedPrediction);
            Assert.Equal(3, updatedPrediction.Team1_predicted_goals);
            Assert.Equal(2, updatedPrediction.Team2_predicted_goals);
        }

        [Fact]
        public async Task Delete_ShouldRemovePrediction()
        {
            // Arrange
            var prediction = new Prediction { Name = "Football Match", Team1_predicted_goals = 2, Team2_predicted_goals = 1 };
            await _predictionService.Create(prediction);

            // Act
            await _predictionService.Delete(prediction.Id);

            // Assert
            var result = await _predictionService.Get(prediction.Id);
            Assert.Null(result);
        }
    }
}
