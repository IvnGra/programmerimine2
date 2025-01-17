using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using KooliProjekt.Controllers;
using KooliProjekt.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Moq;
using Xunit;

namespace KooliProjekt.UnitTests.ControllerTests
{
    public class PredictionsControllerTests
    {
        private readonly Mock<ApplicationDbContext> _contextMock;
        private readonly PredictionsController _controller;

        public PredictionsControllerTests()
        {
        
            _contextMock = new Mock<ApplicationDbContext>();
            _controller = new PredictionsController(_contextMock.Object);
        }

        [Fact]
        public async Task Index_Should_Return_Correct_View_With_Data()
        {
            // Arrange
            var predictions = new List<Prediction>
            {
                new Prediction { Id = 1, Team1_predicted_goals = 2, Team2_predicted_goals = 1 },
                new Prediction { Id = 2, Team1_predicted_goals = 3, Team2_predicted_goals = 2 }
            };

            var mockSet = new Mock<DbSet<Prediction>>();
            mockSet.As<IQueryable<Prediction>>().Setup(m => m.Provider).Returns(predictions.AsQueryable().Provider);
            mockSet.As<IQueryable<Prediction>>().Setup(m => m.Expression).Returns(predictions.AsQueryable().Expression);
            mockSet.As<IQueryable<Prediction>>().Setup(m => m.ElementType).Returns(predictions.AsQueryable().ElementType);
            mockSet.As<IQueryable<Prediction>>().Setup(m => m.GetEnumerator()).Returns(predictions.GetEnumerator());

            _contextMock.Setup(c => c.Predictions).Returns(mockSet.Object);

            // Act
            var result = await _controller.Index() as ViewResult;

            // Assert
            Assert.NotNull(result);
            Assert.NotNull(result.Model);
            Assert.IsType<List<Prediction>>(result.Model);
            var model = result.Model as List<Prediction>;
            Assert.Equal(2, model.Count);
        }
    }
}