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
    public class LeaderboardsControllerTests
    {
        private readonly Mock<ApplicationDbContext> _contextMock;
        private readonly LeaderboardsController _controller;

        public LeaderboardsControllerTests()
        {


            _contextMock = new Mock<ApplicationDbContext>(options);
            _controller = new LeaderboardsController(_contextMock.Object);
        }

        private ApplicationDbContext options()
        {
            throw new NotImplementedException();
        }

        [Fact]
        public async Task Index_should_return_correct_view_with_data()
        {
            // Arrange
            var leaderboards = new List<Leaderboard>
            {
                new Leaderboard { Id = 1, Name = "Test Leaderboard 1" },
                new Leaderboard { Id = 2, Name = "Test Leaderboard 2" }
            };

            var mockSet = new Mock<DbSet<Leaderboard>>();
            mockSet.As<IQueryable<Leaderboard>>().Setup(m => m.Provider).Returns(leaderboards.AsQueryable().Provider);
            mockSet.As<IQueryable<Leaderboard>>().Setup(m => m.Expression).Returns(leaderboards.AsQueryable().Expression);
            mockSet.As<IQueryable<Leaderboard>>().Setup(m => m.ElementType).Returns(leaderboards.AsQueryable().ElementType);
            mockSet.As<IQueryable<Leaderboard>>().Setup(m => m.GetEnumerator()).Returns(leaderboards.GetEnumerator());

            _contextMock.Setup(c => c.Leaderboards).Returns(mockSet.Object);

            // Act
            var result = await _controller.Index() as ViewResult;

            // Assert
            Assert.NotNull(result);
            Assert.NotNull(result.Model);
            Assert.IsType<List<Leaderboard>>(result.Model);
            var model = result.Model as List<Leaderboard>;
            Assert.Equal(2, model.Count);
        }
    }
}

