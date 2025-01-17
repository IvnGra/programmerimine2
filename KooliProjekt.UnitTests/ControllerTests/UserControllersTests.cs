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
    public class UsersControllerTests
    {
        private readonly Mock<ApplicationDbContext> _contextMock;
        private readonly UsersController _controller;

        public UsersControllerTests()
        {
          
            _contextMock = new Mock<ApplicationDbContext>();
            _controller = new UsersController(_contextMock.Object);
        }

        [Fact]
        public async Task Index_Should_Return_Correct_View_With_Data()
        {
            // Arrange
            var users = new List<User>
            {
                new User { Id = 1, Username = "User1", IsAdmin = false },
                new User { Id = 2, Username = "User2", IsAdmin = true }
            };

            var mockSet = new Mock<DbSet<User>>();
            mockSet.As<IQueryable<User>>().Setup(m => m.Provider).Returns(users.AsQueryable().Provider);
            mockSet.As<IQueryable<User>>().Setup(m => m.Expression).Returns(users.AsQueryable().Expression);
            mockSet.As<IQueryable<User>>().Setup(m => m.ElementType).Returns(users.AsQueryable().ElementType);
            mockSet.As<IQueryable<User>>().Setup(m => m.GetEnumerator()).Returns(users.GetEnumerator());

            _contextMock.Setup(c => c.Users).Returns(mockSet.Object);

            // Act
            var result = await _controller.Index() as ViewResult;

            // Assert
            Assert.NotNull(result);
            Assert.NotNull(result.Model);
            Assert.IsType<List<User>>(result.Model);
            var model = result.Model as List<User>;
            Assert.Equal(2, model.Count);
        }

        [Fact]
        public async Task Details_Should_Return_Correct_View_When_User_Exists()
        {
            // Arrange
            var user = new User { Id = 1, Username = "User1", IsAdmin = false };

            _contextMock.Setup(c => c.Users.FindAsync(1))
                .ReturnsAsync(user);

            // Act
            var result = await _controller.Details(1) as ViewResult;

            // Assert
            Assert.NotNull(result);
            Assert.NotNull(result.Model);
            Assert.IsType<User>(result.Model);
            var model = result.Model as User;
            Assert.Equal(1, model.Id);
        }

        [Fact]
        public async Task Details_Should_Return_NotFound_When_User_Does_Not_Exist()
        {
            // Arrange
            _contextMock.Setup(c => c.Users.FindAsync(It.IsAny<int>()))
                .ReturnsAsync((User)null);

            // Act
            var result = await _controller.Details(1);

            // Assert
            Assert.IsType<NotFoundResult>(result);
        }

        [Fact]
        public async Task Create_Should_Add_New_User_And_Redirect()
        {
            // Arrange
            var user = new User { Id = 1, Username = "NewUser", IsAdmin = false };

            // Act
            var result = await _controller.Create(user) as RedirectToActionResult;

            // Assert
            Assert.NotNull(result);
            Assert.Equal("Index", result.ActionName);
            _contextMock.Verify(c => c.Add(user), Times.Once);
            _contextMock.Verify(c => c.SaveChangesAsync(It.IsAny<CancellationToken>()), Times.Once);
        }

        [Fact]
        public async Task Edit_Should_Update_User_And_Redirect()
        {
            // Arrange
            var user = new User { Id = 1, Username = "UpdatedUser", IsAdmin = true };

            _contextMock.Setup(c => c.Users.FindAsync(1)).ReturnsAsync(user);

            // Act
            var result = await _controller.Edit(1, user) as RedirectToActionResult;

            // Assert
            Assert.NotNull(result);
            Assert.Equal("Index", result.ActionName);
            _contextMock.Verify(c => c.Update(user), Times.Once);
            _contextMock.Verify(c => c.SaveChangesAsync(It.IsAny<CancellationToken>()), Times.Once);
        }

        [Fact]
        public async Task Edit_Should_Return_NotFound_If_User_Does_Not_Exist()
        {
            // Arrange
            _contextMock.Setup(c => c.Users.FindAsync(It.IsAny<int>()))
                .ReturnsAsync((User)null);

            var user = new User { Id = 1, Username = "NonExistentUser", IsAdmin = false };

            // Act
            var result = await _controller.Edit(1, user);

            // Assert
            Assert.IsType<NotFoundResult>(result);
        }

        [Fact]
        public async Task DeleteConfirmed_Should_Remove_User_And_Redirect()
        {
            // Arrange
            var user = new User { Id = 1, Username = "UserToDelete", IsAdmin = false };

            _contextMock.Setup(c => c.Users.FindAsync(1)).ReturnsAsync(user);

            // Act
            var result = await _controller.DeleteConfirmed(1) as RedirectToActionResult;

            // Assert
            Assert.NotNull(result);
            Assert.Equal("Index", result.ActionName);
            _contextMock.Verify(c => c.Users.Remove(user), Times.Once);
            _contextMock.Verify(c => c.SaveChangesAsync(It.IsAny<CancellationToken>()), Times.Once);
        }
    }
}