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
    public class TeamsControllerTests
    {
        private readonly Mock<ApplicationDbContext> _contextMock;
        private readonly TeamsController _controller;

        public TeamsControllerTests()
        {
            _contextMock = new Mock<ApplicationDbContext>();
            _controller = new TeamsController(_contextMock.Object);
        }

        [Fact]
        public async Task Index_Should_Return_Correct_View_With_Data()
        {
            // Arrange
            var teams = new List<Team>
            {
                new Team { Id = 1, TeamName = "Team A" },
                new Team { Id = 2, TeamName = "Team B" }
            };

            var mockSet = new Mock<DbSet<Team>>();
            mockSet.As<IQueryable<Team>>().Setup(m => m.Provider).Returns(teams.AsQueryable().Provider);
            mockSet.As<IQueryable<Team>>().Setup(m => m.Expression).Returns(teams.AsQueryable().Expression);
            mockSet.As<IQueryable<Team>>().Setup(m => m.ElementType).Returns(teams.AsQueryable().ElementType);
            mockSet.As<IQueryable<Team>>().Setup(m => m.GetEnumerator()).Returns(teams.GetEnumerator());

            _contextMock.Setup(c => c.Teams).Returns(mockSet.Object);

            // Act
            var result = await _controller.Index() as ViewResult;

            // Assert
            Assert.NotNull(result);
            Assert.NotNull(result.Model);
            Assert.IsType<List<Team>>(result.Model);
            var model = result.Model as List<Team>;
            Assert.Equal(2, model.Count);
        } }}