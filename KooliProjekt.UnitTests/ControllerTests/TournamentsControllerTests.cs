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

        [Fact]
        public async Task Details_Should_Return_Correct_View_When_Tournament_Exists()
        {
            // Arrange
            var tournament = new Tournament { Id = 1, TournamentName = "Tournament 1" };

            _contextMock.Setup(c => c.Tournaments.FindAsync(1))
                .ReturnsAsync(tournament);

            // Act
            var result = await _controller.Details(1) as ViewResult;

            // Assert
            Assert.NotNull(result);
            Assert.NotNull(result.Model);
            Assert.IsType<Tournament>(result.Model);
            var model = result.Model as Tournament;
            Assert.Equal(1, model.Id);
        }

        [Fact]
        public async Task Details_Should_Return_NotFound_When_Tournament_Does_Not_Exist()
        {
            // Arrange
            _contextMock.Setup(c => c.Tournaments.FindAsync(It.IsAny<int>()))
                .ReturnsAsync((Tournament)null);

            // Act
            var result = await _controller.Details(1);

            // Assert
            Assert.IsType<NotFoundResult>(result);
        }

        [Fact]
        public async Task Create_Should_Add_Tournament_And_Redirect_To_Index()
        {
            // Arrange
            var tournament = new Tournament { Id = 1, TournamentName = "Tournament 1" };

            _contextMock.Setup(c => c.Add(tournament)).Verifiable();
            _contextMock.Setup(c => c.SaveChangesAsync(default)).ReturnsAsync(1);

            // Act
            var result = await _controller.Create(tournament) as RedirectToActionResult;

            // Assert
            _contextMock.Verify(c => c.Add(tournament), Times.Once);
            _contextMock.Verify(c => c.SaveChangesAsync(default), Times.Once);
            Assert.NotNull(result);
            Assert.Equal("Index", result.ActionName);
        }

        [Fact]
        public async Task Edit_Should_Update_Tournament_And_Redirect_To_Index()
        {
            // Arrange
            var tournament = new Tournament { Id = 1, TournamentName = "Tournament 1" };

            _contextMock.Setup(c => c.Tournaments.FindAsync(1))
                .ReturnsAsync(tournament);
            _contextMock.Setup(c => c.Update(tournament)).Verifiable();
            _contextMock.Setup(c => c.SaveChangesAsync(default)).ReturnsAsync(1);

            // Act
            var result = await _controller.Edit(1, tournament) as RedirectToActionResult;

            // Assert
            _contextMock.Verify(c => c.Update(tournament), Times.Once);
            _contextMock.Verify(c => c.SaveChangesAsync(default), Times.Once);
            Assert.NotNull(result);
            Assert.Equal("Index", result.ActionName);
        }

        [Fact]
        public async Task DeleteConfirmed_Should_Remove_Tournament_And_Redirect_To_Index()
        {
            // Arrange
            var tournament = new Tournament { Id = 1, TournamentName = "Tournament 1" };

            _contextMock.Setup(c => c.Tournaments.FindAsync(1))
                .ReturnsAsync(tournament);
            _contextMock.Setup(c => c.Tournaments.Remove(tournament)).Verifiable();
            _contextMock.Setup(c => c.SaveChangesAsync(default)).ReturnsAsync(1);

            // Act
            var result = await _controller.DeleteConfirmed(1) as RedirectToActionResult;

            // Assert
            _contextMock.Verify(c => c.Tournaments.Remove(tournament), Times.Once);
            _contextMock.Verify(c => c.SaveChangesAsync(default), Times.Once);
            Assert.NotNull(result);
            Assert.Equal("Index", result.ActionName);
        }
    }
}
