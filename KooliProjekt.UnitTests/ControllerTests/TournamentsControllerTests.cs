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
    public class TournamentsControllerTests
    {
        private readonly Mock<ApplicationDbContext> _contextMock;
        private readonly TournamentsController _controller;

        public TournamentsControllerTests()
        {


            _contextMock = new Mock<ApplicationDbContext>();
            _controller = new TournamentsController(_contextMock.Object);
        }

        [Fact]
        public async Task Index_Should_Return_Correct_View_With_Data()
        {
            // Arrange
            var tournaments = new List<Tournament>
            {
                new Tournament { Id = 1, TournamentName = "Tournament 1" },
                new Tournament { Id = 2, TournamentName = "Tournament 2" }
            };

            var mockSet = new Mock<DbSet<Tournament>>();
            mockSet.As<IQueryable<Tournament>>().Setup(m => m.Provider).Returns(tournaments.AsQueryable().Provider);
            mockSet.As<IQueryable<Tournament>>().Setup(m => m.Expression).Returns(tournaments.AsQueryable().Expression);
            mockSet.As<IQueryable<Tournament>>().Setup(m => m.ElementType).Returns(tournaments.AsQueryable().ElementType);
            mockSet.As<IQueryable<Tournament>>().Setup(m => m.GetEnumerator()).Returns(tournaments.GetEnumerator());

            _contextMock.Setup(c => c.Tournaments).Returns(mockSet.Object);

            // Act
            var result = await _controller.Index() as ViewResult;

            // Assert
            Assert.NotNull(result);
            Assert.NotNull(result.Model);
            Assert.IsType<List<Tournament>>(result.Model);
            var model = result.Model as List<Tournament>;
            Assert.Equal(2, model.Count);
        }

    }
}