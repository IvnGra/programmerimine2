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
using Match = KooliProjekt.Data.Match;

namespace KooliProjekt.UnitTests.ControllerTests
{
    public class MatchesControllerTests
    {
        private readonly Mock<ApplicationDbContext> _contextMock;
        private readonly MatchesController _controller;

        public MatchesControllerTests()
        {


            _contextMock = new Mock<ApplicationDbContext>();
            _controller = new MatchesController(_contextMock.Object);
        }

        [Fact]
        public async Task Index_Should_Return_Correct_View_With_Data()
        {
            // Arrange
            var matches = new List<Match>
            {
                new Match { Id = 1, Round = "Round 1" },
                new Match { Id = 2, Round = "Round 2" }
            };

            var mockSet = new Mock<DbSet<Match>>();
            mockSet.As<IQueryable<Match>>().Setup(m => m.Provider).Returns(matches.AsQueryable().Provider);
            mockSet.As<IQueryable<Match>>().Setup(m => m.Expression).Returns(matches.AsQueryable().Expression);
            mockSet.As<IQueryable<Match>>().Setup(m => m.ElementType).Returns(matches.AsQueryable().ElementType);
            mockSet.As<IQueryable<Match>>().Setup(m => m.GetEnumerator()).Returns(matches.GetEnumerator());

            _contextMock.Setup(c => c.Matchs).Returns(mockSet.Object);

            // Act
            var result = await _controller.Index() as ViewResult;

            // Assert
            Assert.NotNull(result);
            Assert.NotNull(result.Model);
            Assert.IsType<List<Match>>(result.Model);
            var model = result.Model as List<Match>;
            Assert.Equal(2, model.Count);
        }
    }
}

       