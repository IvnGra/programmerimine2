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
        [Fact]
        public async Task Create_should_return_view_when_model_is_invalid()
        {
            // Arrange
            _controller.ModelState.AddModelError("Name", "Name is required");

            // Act
            var result = await _controller.Create(new Leaderboard()) as ViewResult;

            // Assert
            Assert.NotNull(result);
            Assert.True(!_controller.ModelState.IsValid);
        }

        [Fact]
        public async Task Create_should_redirect_to_index_when_model_is_valid()
        {
            // Arrange
            var newLeaderboard = new Leaderboard { Name = "New Leaderboard", place = "pärnu mnt18", Score = 52 };
            _LeaderboardServiceMock.Setup(x => x.Create(It.IsAny<Leaderboard>())).Returns(Task.CompletedTask);

            // Act
            var result = await _controller.Create(newLeaderboard) as RedirectToActionResult;

            // Assert
            Assert.NotNull(result);
            Assert.Equal("Index", result.ActionName);
        }

        [Fact]
        public async Task Edit_should_return_not_found_when_leaderboard_does_not_exist()
        {
            // Arrange
            int id = 999;
            _LeaderboardServiceMock.Setup(x => x.Get(id)).ReturnsAsync((Leaderboard)null);

            // Act
            var result = await _controller.Edit(id);

            // Assert
            Assert.IsType<NotFoundResult>(result);
        }

        [Fact]
        public async Task Delete_should_redirect_to_index_when_leaderboard_deleted()
        {
            // Arrange
            int id = 1;
            var leaderboard = new Leaderboard { Name = "New Leaderboard", place = "pärnu mnt18", Score = 52 };
            _LeaderboardServiceMock.Setup(x => x.Delete(id)).Returns(Task.CompletedTask);

            // Act
            var result = await _controller.DeleteConfirmed(id) as RedirectToActionResult;

            // Assert
            Assert.NotNull(result);
            Assert.Equal("Index", result.ActionName);
        }

        [Fact]
        public async Task Delete_should_return_not_found_when_leaderboard_not_found()
        {
            // Arrange
            int id = 999;
            _LeaderboardServiceMock.Setup(x => x.Get(id)).ReturnsAsync((Leaderboard)null);

            // Act
            var result = await _controller.Delete(id);

            // Assert
            Assert.IsType<NotFoundResult>(result);
        }

    }
}
