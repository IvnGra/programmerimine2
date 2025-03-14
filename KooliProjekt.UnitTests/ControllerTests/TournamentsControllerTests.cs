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

    }
}