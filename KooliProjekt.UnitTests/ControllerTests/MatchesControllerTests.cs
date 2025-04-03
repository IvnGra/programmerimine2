using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Text;
using System.Threading.Tasks;
using KooliProjekt.Controllers;
using KooliProjekt.Data;
using KooliProjekt.Models;
using KooliProjekt.Search;
using KooliProjekt.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Moq;
using Xunit;
using static System.Runtime.InteropServices.JavaScript.JSType;
using Match = KooliProjekt.Data.Match;

namespace KooliProjekt.UnitTests.ControllerTests
{
    public class MatchesControllerTests
    {
        private readonly Mock<IMatchesService> _MatchServiceMock;
        private readonly MatchesController _controller;

        public MatchesControllerTests()
        {


            _MatchServiceMock = new Mock<IMatchesService>();
            _controller = new MatchesController(_MatchServiceMock.Object);
        }

        [Fact]
        public async Task Index_Should_Return_Correct_View_With_Data()
        {
            // Arrange
            int page = 1;
            var data = new List<Match>
            {
                new Match { Id = 1, Round = 1 },
                new Match { Id = 2, Round = 2 }
            };

            var pagedResult = new KooliProjekt.Data.PagedResult<Match> { Results = data };


            _MatchServiceMock.Setup(x => x.List(page, 5, It.IsAny<MatchesSearch>())).ReturnsAsync(pagedResult);

            // Act
            var result = await _controller.Index(page) as ViewResult;

            // Assert
            Assert.NotNull(result);
            var model = result.Model as MatchesIndexModel ;
            Assert.NotNull(model);
            Assert.Equal(pagedResult, model.Data);
        }
        [Fact]
        public async Task Details_should_return_view_with_model_when_match_found()
        {
            // Arrange
            int id = 1;
            var match = new Match { Id = id, Match_time = DateTime.Now };
            _MatchServiceMock.Setup(x => x.Get(id)).ReturnsAsync(match);

            // Act
            var result = await _controller.Details(id) as ViewResult;

            // Assert
            Assert.NotNull(result);  // Ensure the result is a ViewResult
            Assert.Equal(match, result.Model);  // Assert that the model returned matches the expected match
        }

        [Fact]
        public async Task Edit_should_return_view_with_model_when_match_found()
        {
            // Arrange
            int id = 1;
            var match = new Match { Id = id, Match_time = DateTime.Now };
            _MatchServiceMock.Setup(x => x.Get(id)).ReturnsAsync(match);

            // Act
            var result = await _controller.Edit(id) as ViewResult;

            // Assert
            Assert.NotNull(result);  // Ensure the result is a ViewResult
            Assert.Equal(match, result.Model);  // Assert that the model returned matches the expected match
        }

        [Fact]
        public async Task Delete_should_return_view_with_model_when_match_found()
        {
            // Arrange
            int id = 1;
            var match = new Match { Id = id, Match_time = DateTime.Now };
            _MatchServiceMock.Setup(x => x.Get(id)).ReturnsAsync(match);

            // Act
            var result = await _controller.Delete(id) as ViewResult;

            // Assert
            Assert.NotNull(result);  // Ensure the result is a ViewResult
            Assert.Equal(match, result.Model);  // Assert that the model returned matches the expected match
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
            var result = await _controller.Create(new Match()) as ViewResult;

            // Assert
            Assert.NotNull(result);
            Assert.True(!_controller.ModelState.IsValid);
        }

        [Fact]
        public async Task Create_should_redirect_to_index_when_model_is_valid()
        {
            // Arrange
            var newMatch = new Match { Name = "Match 1", Round = 6 };
            _MatchServiceMock.Setup(x => x.Create(It.IsAny<Match>())).Returns(Task.CompletedTask);

            // Act
            var result = await _controller.Create(newMatch) as RedirectToActionResult;

            // Assert
            Assert.NotNull(result);
            Assert.Equal("Index", result.ActionName);
        }

        [Fact]
        public async Task Edit_should_return_not_found_when_match_does_not_exist()
        {
            // Arrange
            int id = 999;
            _MatchServiceMock.Setup(x => x.Get(id)).ReturnsAsync((Match)null);

            // Act
            var result = await _controller.Edit(id);

            // Assert
            Assert.IsType<NotFoundResult>(result);
        }

        [Fact]
        public async Task Delete_should_redirect_to_index_when_match_deleted()
        {
            // Arrange
            int id = 1;
            var match = new Match { Id = id, Name = "Match 1", Round = 6 };
            _MatchServiceMock.Setup(x => x.Delete(id)).Returns(Task.CompletedTask);

            // Act
            var result = await _controller.DeleteConfirmed(id) as RedirectToActionResult;

            // Assert
            Assert.NotNull(result);
            Assert.Equal("Index", result.ActionName);
        }

        [Fact]
        public async Task Delete_should_return_not_found_when_match_not_found()
        {
            // Arrange
            int id = 999;
            _MatchServiceMock.Setup(x => x.Get(id)).ReturnsAsync((Match)null);

            // Act
            var result = await _controller.Delete(id);

            // Assert
            Assert.IsType<NotFoundResult>(result);
        }

    }
}


       