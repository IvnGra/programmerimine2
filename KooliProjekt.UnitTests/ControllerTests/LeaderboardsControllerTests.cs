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
    public class LeaderboardsControllerTests
    {
        private readonly Mock<ILeaderboardService> _LeaderboardServiceMock;
        private readonly LeaderboardsController _controller;

        public LeaderboardsControllerTests()
        {
            _LeaderboardServiceMock = new Mock<ILeaderboardService>();
            _controller = new LeaderboardsController(_LeaderboardServiceMock.Object);
        }

        [Fact]
        public async Task Index_should_return_correct_view_with_data()
        {
            // Arrange
            int page = 1;
            var data = new List<Leaderboard>
            {
                new Leaderboard { Id = 1, Name = "Test Leaderboard 1" },
                new Leaderboard { Id = 2, Name = "Test Leaderboard 2" }
            };
            var pagedResult = new PagedResult<Leaderboard> { Results = data };

            _LeaderboardServiceMock.Setup(x => x.List(page, 5, It.IsAny<LeaderboardsSearch>())).ReturnsAsync(pagedResult);

            // Act
            var result = await _controller.Index() as ViewResult;

            // Assert
            Assert.NotNull(result);
            var model = result.Model as LeaderboardsIndexModel;
            Assert.NotNull(model);
            Assert.Equal(pagedResult, model.Data);
        }
        [Fact]
        public async Task Details_should_return_view_with_model_when_leaderboard_found()
        {
                // Arrange
                int id = 1;
                var leaderboard = new Leaderboard { Id = id, Name = "Team 1", Score = 30};
                _LeaderboardServiceMock.Setup(x => x.Get(id)).ReturnsAsync(leaderboard);

                // Act
                var result = await _controller.Details(id) as ViewResult;

                // Assert
                Assert.NotNull(result);  // Ensure the result is a ViewResult
                Assert.Equal(leaderboard, result.Model);  // Assert that the model returned matches the expected leaderboard
        }

        [Fact]
        public async Task Edit_should_return_view_with_model_when_leaderboard_found()
            {
                // Arrange
                int id = 1;
                var leaderboard = new Leaderboard { Id = id, Name = "Team 1", Score = 30 };
                _LeaderboardServiceMock.Setup(x => x.Get(id)).ReturnsAsync(leaderboard);

                // Act
                var result = await _controller.Edit(id) as ViewResult;

                // Assert
                Assert.NotNull(result);  // Ensure the result is a ViewResult
                Assert.Equal(leaderboard, result.Model);  // Assert that the model returned matches the expected leaderboard
            }

        [Fact]
        public async Task Delete_should_return_view_with_model_when_leaderboard_found()
            {
                // Arrange
                int id = 1;
                var leaderboard = new Leaderboard { Id = id, Name = "Team 1", Score = 30 };
                _LeaderboardServiceMock.Setup(x => x.Get(id)).ReturnsAsync(leaderboard);

                // Act
                var result = await _controller.Delete(id) as ViewResult;

                // Assert
                Assert.NotNull(result);  // Ensure the result is a ViewResult
                Assert.Equal(leaderboard, result.Model);  // Assert that the model returned matches the expected leaderboard
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
