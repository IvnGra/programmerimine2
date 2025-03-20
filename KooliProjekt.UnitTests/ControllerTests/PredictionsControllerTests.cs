using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using KooliProjekt.Controllers;
using KooliProjekt.Data;
using KooliProjekt.Models;
using KooliProjekt.Search;
using KooliProjekt.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Moq;
using Xunit;

namespace KooliProjekt.UnitTests.ControllerTests
{
    public class PredictionsControllerTests
    {
        private readonly Mock<IPredictionService> _PredictionServiceMock;
        private readonly PredictionsController _controller;

        public PredictionsControllerTests()
        {
        
            _PredictionServiceMock = new Mock<IPredictionService>();
            _controller = new PredictionsController(_PredictionServiceMock.Object);
        }

        [Fact]
        public async Task Index_Should_Return_Correct_View_With_Data()
        {
            // Arrange
            int page = 1;
            var data = new List<Prediction>
            {
                new Prediction { Id = 1, Team1_predicted_goals = 2, Team2_predicted_goals = 1 },
                new Prediction { Id = 2, Team1_predicted_goals = 3, Team2_predicted_goals = 2 }
            };

            var pagedResult = new PagedResult<Prediction> { Results = data };

            _PredictionServiceMock.Setup(x => x.List(page, 5, It.IsAny<PredictionsSearch>())).ReturnsAsync(pagedResult);

            // Act
            var result = await _controller.Index(page) as ViewResult;

            // Assert
            Assert.NotNull(result);
            var model = result.Model as PredictionsIndexModel;
            Assert.NotNull(model);
            Assert.Equal(pagedResult, model.Data);
        }

            [Fact]
            public async Task Details_should_return_view_with_model_when_prediction_found()
            {
                // Arrange
                int id = 1;
                var prediction = new Prediction { Id = id, MatchId = 1, UserId = 1,Points= 10, PointsEarned = 3, Team1_predicted_goals = 3, Team2_predicted_goals = 5 };
                _PredictionServiceMock.Setup(x => x.Get(id)).ReturnsAsync(prediction);

                // Act
                var result = await _controller.Details(id) as ViewResult;

                // Assert
                Assert.NotNull(result);  // Ensure the result is a ViewResult
                Assert.Equal(prediction, result.Model);  // Assert that the model returned matches the expected prediction
            }

            [Fact]
            public async Task Edit_should_return_view_with_model_when_prediction_found()
            {
                // Arrange
                int id = 1;
                var prediction = new Prediction { Id = id, MatchId = 1, UserId = 1, Points = 10, PointsEarned = 3, Team1_predicted_goals = 3, Team2_predicted_goals = 5 };
                _PredictionServiceMock.Setup(x => x.Get(id)).ReturnsAsync(prediction);

                // Act
                var result = await _controller.Edit(id) as ViewResult;

                // Assert
                Assert.NotNull(result);  // Ensure the result is a ViewResult
                Assert.Equal(prediction, result.Model);  // Assert that the model returned matches the expected prediction
            }

            [Fact]
            public async Task Delete_should_return_view_with_model_when_prediction_found()
            {
                // Arrange
                int id = 1;
                var prediction = new Prediction { Id = id, MatchId = 1, UserId = 1, Points = 10, PointsEarned = 3, Team1_predicted_goals = 3, Team2_predicted_goals = 5 };
                _PredictionServiceMock.Setup(x => x.Get(id)).ReturnsAsync(prediction);

                // Act
                var result = await _controller.Delete(id) as ViewResult;

                // Assert
                Assert.NotNull(result);  // Ensure the result is a ViewResult
                Assert.Equal(prediction, result.Model);  // Assert that the model returned matches the expected prediction
            }

            [Fact]
            public void Create_should_return_view()
            {
                // Act
                var result = _controller.Create() as ViewResult;

                // Assert
                Assert.NotNull(result);  // Ensure the result is a ViewResult

            }   
    }
}