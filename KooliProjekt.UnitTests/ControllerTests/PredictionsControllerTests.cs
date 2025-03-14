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
    }
}