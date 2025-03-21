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
    public class UsersControllerTests
    {
        private readonly Mock<IUserService> _userServiceMock;
        private readonly UsersController _controller;

        public UsersControllerTests()
        {

            _userServiceMock = new Mock<IUserService>();
            _controller = new UsersController(_userServiceMock.Object);
        }

        [Fact]  
        public async Task Index_Should_Return_Correct_View_With_Data()
        {
            // Arrange
            int page = 1;
            var data = new List<User>
            {
                new User { Id = 1, Username = "User1", IsAdmin = false },
                new User { Id = 2, Username = "User2", IsAdmin = true }
            };

            var pagedResult = new KooliProjekt.Data.PagedResult<User> { Results = data };


            // Setup mock service to return the paged result
            _userServiceMock.Setup(x => x.List(page, 5, It.IsAny<UsersSearch>())).ReturnsAsync(pagedResult);

            // Act
            var result = await _controller.Index(page) as ViewResult;

            // Assert
            Assert.NotNull(result);
            var model = result.Model as UsersIndexModel;
            Assert.NotNull(model);
            Assert.Equal(pagedResult, model.Data);
        }

        [Fact]
        public async Task Details_should_return_view_with_model_when_user_found()
        {
            // Arrange
            int id = 1;
            var user = new User { Id = id, Username = "User 1", UserEmail = "user@email.com", IsAdmin = true };
            _userServiceMock.Setup(x => x.Get(id)).ReturnsAsync(user);  // Mock the service to return the user

            // Act
            var result = await _controller.Details(id) as ViewResult;  // Await the result here

            // Assert
            Assert.NotNull(result);  // Ensure the result is a ViewResult
            Assert.Equal(user, result.Model);  // Assert that the model returned matches the expected user
        }

        [Fact]
        public async Task Edit_should_return_view_with_model_when_user_found()
        {
            // Arrange
            int id = 1;
            var user = new User { Id = id, Username = "User 1", UserEmail = "user@email.com", IsAdmin = true };
            _userServiceMock.Setup(x => x.Get(id)).ReturnsAsync(user);

            // Act
            var result = await _controller.Edit(id) as ViewResult;

            // Assert
            Assert.NotNull(result);  // Ensure the result is a ViewResult

            // Check if the model returned is the correct user
            var model = result.Model as User;
            Assert.NotNull(model);  // Ensure the model is not null
            Assert.Equal(user, model);  // Assert that the model returned matches the expected user
        }


        [Fact]
            public async Task Delete_should_return_view_with_model_when_user_found()
            {
                // Arrange
                int id = 1;
                var user = new User { Id = id, Username = "User 1", UserEmail = "user@email.com", IsAdmin = true };
                _userServiceMock.Setup(x => x.Get(id)).ReturnsAsync(user);

                // Act
                var result = await _controller.Delete(id) as ViewResult;

                // Assert
                Assert.NotNull(result);  // Ensure the result is a ViewResult
                Assert.Equal(user, result.Model);  // Assert that the model returned matches the expected user
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
            var result = await _controller.Create(new User()) as ViewResult;

            // Assert
            Assert.NotNull(result);
            Assert.True(!_controller.ModelState.IsValid);
        }

        [Fact]
        public async Task Create_should_redirect_to_index_when_model_is_valid()
        {
            // Arrange
            var newUser = new User { Username = "User 1", UserEmail = "user@email.com", IsAdmin = true };
            _userServiceMock.Setup(x => x.Create(It.IsAny<User>())).Returns(Task.CompletedTask);

            // Act
            var result = await _controller.Create(newUser) as RedirectToActionResult;

            // Assert
            Assert.NotNull(result);
            Assert.Equal("Index", result.ActionName);
        }

        [Fact]
        public async Task Edit_should_return_not_found_when_user_does_not_exist()
        {
            // Arrange
            int id = 999;
            _userServiceMock.Setup(x => x.Get(id)).ReturnsAsync((User)null);

            // Act
            var result = await _controller.Edit(id);

            // Assert
            Assert.IsType<NotFoundResult>(result);
        }

        [Fact]
        public async Task Delete_should_redirect_to_index_when_user_deleted()
        {
            // Arrange
            int id = 1;
            var user = new User { Id = id, Username = "User 1", UserEmail = "user@email.com", IsAdmin = true };
            _userServiceMock.Setup(x => x.Delete(id)).Returns(Task.CompletedTask);

            // Act
            var result = await _controller.DeleteConfirmed(id) as RedirectToActionResult;

            // Assert
            Assert.NotNull(result);
            Assert.Equal("Index", result.ActionName);
        }

        [Fact]
        public async Task Delete_should_return_not_found_when_user_not_found()
        {
            // Arrange
            int id = 999;
            _userServiceMock.Setup(x => x.Get(id)).ReturnsAsync((User)null);

            // Act
            var result = await _controller.Delete(id);

            // Assert
            Assert.IsType<NotFoundResult>(result);
        }

    }
}