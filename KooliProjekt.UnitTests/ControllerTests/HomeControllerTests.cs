using KooliProjekt.Controllers;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq; // Import Moq for mocking dependencies
using Xunit;

namespace KooliProjekt.UnitTests.ControllerTests
{
    public class HomeControllerTests
    {
        private readonly HomeController _controller;

        public HomeControllerTests()
        {
            // Create a mock logger
            var mockLogger = new Mock<ILogger<HomeController>>();

            // Pass the mock logger to the controller
            _controller = new HomeController(mockLogger.Object);
        }

        [Fact]
        public void Index_should_return_index_view()
        {
            // Act
            var result = _controller.Index() as ViewResult;

            // Assert
            Assert.NotNull(result);
            Assert.True(result.ViewName == "Index" ||
                        string.IsNullOrEmpty(result.ViewName));
        }

        [Fact]
        public void Privacy_should_return_privacy_view()
        {
            // Act
            var result = _controller.Privacy() as ViewResult; // Fix: Call Privacy(), not Index()

            // Assert
            Assert.NotNull(result);
            Assert.True(result.ViewName == "Privacy" ||
                        string.IsNullOrEmpty(result.ViewName));
        }

        [Fact]
        public void Error_should_return_error_view()
        {
            // Set up a fake HttpContext
            _controller.ControllerContext = new ControllerContext();
            _controller.ControllerContext.HttpContext = new DefaultHttpContext();

            // Act
            var result = _controller.Error() as ViewResult; // Fix: Call Error(), not Index()

            // Assert
            Assert.NotNull(result);
            Assert.True(result.ViewName == "Error" ||
                        string.IsNullOrEmpty(result.ViewName));
        }
    }
}
