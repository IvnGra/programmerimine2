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
                new Match { Id = 1, Round = "Round 1" },
                new Match { Id = 2, Round = "Round 2" }
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
            var match = new Match { Id = id, Team1_name = "Team 1", Team2_name = "Team 2", Match_time = DateTime.Now };
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
            var match = new Match { Id = id, Team1_name = "Team 1", Team2_name = "Team 2", Match_time = DateTime.Now };
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
            var match = new Match { Id = id, Team1_name = "Team 1", Team2_name = "Team 2", Match_time = DateTime.Now };
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
    }
}


       