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
using Microsoft.EntityFrameworkCore;
using Moq;
using Xunit;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace KooliProjekt.UnitTests.ControllerTests
{
    public class TeamsControllerTests
    {
        private readonly Mock<ITeamsService> _TeamServiceMock;
        private readonly TeamsController _controller;

        public TeamsControllerTests()
        {
            _TeamServiceMock = new Mock<ITeamsService>();
            _controller = new TeamsController(_TeamServiceMock.Object);
        }

        [Fact]
        public async Task Index_Should_Return_Correct_View_With_Data()
        {
            // Arrange
            int page = 1;
            var data = new List<Team>
            {
                new Team { Id = 1, TeamName = "Team A" },
                new Team { Id = 2, TeamName = "Team B" }
            };

            var pagedResult = new PagedResult<Team> { Results = data };


            _TeamServiceMock.Setup(x => x.List(page, 5, It.IsAny<TeamsSearch>())).ReturnsAsync(pagedResult);

            // Act
            var result = await _controller.Index(page) as ViewResult;

            // Assert
            Assert.NotNull(result);
            var model = result.Model as TeamsIndexModel;
            Assert.NotNull(model);
            Assert.Equal(pagedResult, result.Model);
        }
    }
}