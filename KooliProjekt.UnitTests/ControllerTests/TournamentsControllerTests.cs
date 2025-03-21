using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using KooliProjekt.Controllers;
using KooliProjekt.Data;
using KooliProjekt.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Moq;
using Xunit;
using static NuGet.Packaging.PackagingConstants;
using static System.Runtime.InteropServices.JavaScript.JSType;
using KooliProjekt.Search;
using KooliProjekt.Models;

namespace KooliProjekt.UnitTests.ControllerTests
{
    public class TournamentsControllerTests
    {
        private readonly Mock<ITournamentsService> _TournamentServiceMock;
        private readonly TournamentsController _controller;

        public TournamentsControllerTests()
        {
            _TournamentServiceMock = new Mock<ITournamentsService>();
            _controller = new TournamentsController(_TournamentServiceMock.Object);
        }

        [Fact]
        public async Task Index_Should_Return_Correct_View_With_Data()
        {
            // Arrange
            int page = 1;
            var data = new List<Tournament>
            {
                new Tournament { Id = 1, TournamentName = "Tournament 1" },
                new Tournament { Id = 2, TournamentName = "Tournament 2" }
            };

            var pagedResult = new PagedResult<Tournament> { Results = data };  // The expected paged result

            _TournamentServiceMock.Setup(x => x.List(page, 5, It.IsAny<TournamentsSearch>())).ReturnsAsync(pagedResult);

            // Act
            var result = await _controller.Index(page) as ViewResult;

            // Assert
            Assert.NotNull(result);
            var model = result.Model as TournamentsIndexModel;
            Assert.NotNull(model);
            Assert.Equal(pagedResult, model.Data);
        }
        [Fact]
        public async Task Details_should_return_view_with_model_when_tournament_found()
        {
            // Arrange
            int id = 1;
            var tournament = new Tournament { Id = id, TournamentName = "Tournament 1", StartDate = DateTime.Now.AddMonths(1), EndDate = DateTime.Now.AddMonths(2) };
            _TournamentServiceMock.Setup(x => x.Get(id)).ReturnsAsync(tournament);

            // Act
            var result = await _controller.Details(id) as ViewResult;

            // Assert
            Assert.NotNull(result);  // Ensure the result is a ViewResult
            Assert.Equal(tournament, result.Model);  // Assert that the model returned matches the expected tournament
        }

        [Fact]
        public async Task Edit_should_return_view_with_model_when_tournament_found()
        {
            // Arrange
            int id = 1;
            var tournament = new Tournament { Id = id, TournamentName = "Tournament 1", StartDate = DateTime.Now.AddMonths(1), EndDate = DateTime.Now.AddMonths(2) };
            _TournamentServiceMock.Setup(x => x.Get(id)).ReturnsAsync(tournament);

            // Act
            var result = await _controller.Edit(id) as ViewResult;

            // Assert
            Assert.NotNull(result);  // Ensure the result is a ViewResult
            Assert.Equal(tournament, result.Model);  // Assert that the model returned matches the expected tournament
        }

        [Fact]
        public async Task Delete_should_return_view_with_model_when_tournament_found()
        {
            // Arrange
            int id = 1;
            var tournament = new Tournament { Id = id, TournamentName = "Tournament 1", StartDate = DateTime.Now.AddMonths(1), EndDate = DateTime.Now.AddMonths(2) };
            _TournamentServiceMock.Setup(x => x.Get(id)).ReturnsAsync(tournament);

            // Act
            var result = await _controller.Delete(id) as ViewResult;

            // Assert
            Assert.NotNull(result);  // Ensure the result is a ViewResult
            Assert.Equal(tournament, result.Model);  // Assert that the model returned matches the expected tournament
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
            var result = await _controller.Create(new Tournament()) as ViewResult;

            // Assert
            Assert.NotNull(result);
            Assert.True(!_controller.ModelState.IsValid);
        }

        [Fact]
        public async Task Create_should_redirect_to_index_when_model_is_valid()
        {
            // Arrange
            var newTournament = new Tournament { TournamentName = "Tournament 1", StartDate = DateTime.Now.AddMonths(1), EndDate = DateTime.Now.AddMonths(2) };
            _TournamentServiceMock.Setup(x => x.Create(It.IsAny<Tournament>())).Returns(Task.CompletedTask);

            // Act
            var result = await _controller.Create(newTournament) as RedirectToActionResult;

            // Assert
            Assert.NotNull(result);
            Assert.Equal("Index", result.ActionName);
        }

        [Fact]
        public async Task Edit_should_return_not_found_when_tournament_does_not_exist()
        {
            // Arrange
            int id = 999;
            _TournamentServiceMock.Setup(x => x.Get(id)).ReturnsAsync((Tournament)null);

            // Act
            var result = await _controller.Edit(id);

            // Assert
            Assert.IsType<NotFoundResult>(result);
        }

        [Fact]
        public async Task Delete_should_redirect_to_index_when_tournament_deleted()
        {
            // Arrange
            int id = 1;
            var tournament = new Tournament { Id = id, TournamentName = "Tournament 1", StartDate = DateTime.Now.AddMonths(1), EndDate = DateTime.Now.AddMonths(2) };
            _TournamentServiceMock.Setup(x => x.Delete(id)).Returns(Task.CompletedTask);

            // Act
            var result = await _controller.DeleteConfirmed(id) as RedirectToActionResult;

            // Assert
            Assert.NotNull(result);
            Assert.Equal("Index", result.ActionName);
        }

        [Fact]
        public async Task Delete_should_return_not_found_when_tournament_not_found()
        {
            // Arrange
            int id = 999;
            _TournamentServiceMock.Setup(x => x.Get(id)).ReturnsAsync((Tournament)null);

            // Act
            var result = await _controller.Delete(id);

            // Assert
            Assert.IsType<NotFoundResult>(result);
        }


    }
}