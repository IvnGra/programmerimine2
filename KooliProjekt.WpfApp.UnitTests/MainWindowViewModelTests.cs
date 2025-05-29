using Moq;
using System.Collections.Generic;
using System.Threading.Tasks;
using WpfApp1;
using PublicApi.Api;
using Xunit;

namespace WpfApp1.Tests
{
    public class MainWindowViewModelTests
    {
        private readonly Mock<IApiClient> _apiClientMock;
        private readonly MainWindowViewModel _viewModel;

        public MainWindowViewModelTests()
        {
            _apiClientMock = new Mock<IApiClient>();
            _viewModel = new MainWindowViewModel(_apiClientMock.Object);
        }

        [Fact]
        public void NewCommand_ShouldCreateNewUser()
        {
            // Act
            ((RelayCommand<User>)_viewModel.NewCommand).Execute(null);

            // Assert
            Assert.NotNull(_viewModel.SelectedUser);
            Assert.IsType<User>(_viewModel.SelectedUser);
        }

        [Fact]
        public async Task SaveCommand_ShouldSaveUser()
        {
            // Arrange  
            var user = new User { Id = 1, Username = "TestUser" };
            _viewModel.SelectedUser = user;

            _apiClientMock.Setup(api => api.Save(It.IsAny<User>()))
                          .ReturnsAsync(new Result<bool> { Value = true });

            _apiClientMock.Setup(api => api.List<User>())
                          .ReturnsAsync(new Result<List<User>> { Value = new List<User> { user } });

            // Act  
            await Task.Run(() => ((RelayCommand<User>)_viewModel.SaveCommand).Execute(null));

            // Assert  
            _apiClientMock.Verify(api => api.Save(It.IsAny<User>()), Times.Once);
            _apiClientMock.Verify(api => api.List<User>(), Times.Once);
        }

        [Fact]
        public async Task DeleteCommand_ShouldDeleteUser()
        {
            // Arrange  
            var user = new User { Id = 1, Username = "TestUser" };
            _viewModel.Users.Add(user);
            _viewModel.SelectedUser = user;
            _viewModel.ConfirmDelete = u => true;

            _apiClientMock.Setup(api => api.Delete(user.Id))
                          .ReturnsAsync(new Result<bool> { Value = true });

            // Act  
            await Task.Run(() => ((RelayCommand<User>)_viewModel.DeleteCommand).Execute(null));

            // Assert  
            _apiClientMock.Verify(api => api.Delete(user.Id), Times.Once);
            Assert.DoesNotContain(user, _viewModel.Users);
            Assert.Null(_viewModel.SelectedUser);
        }
        // Test method
        [Fact]
        public async Task LoadUsers_ShouldPopulateUsers()
        {
            // Arrange
            var users = new List<User>
    {
        new User { Id = 1, Username = "User 1" },
        new User { Id = 2, Username = "User 2" }
    };

            _apiClientMock.Setup(api => api.List<User>())
                          .ReturnsAsync(new Result<List<User>> { Value = users });

            // Act
            await _viewModel.LoadUsers();

            // Assert
            Assert.Equal(users.Count, _viewModel.Users.Count);

            foreach (var user in users)
            {
                Assert.Contains(_viewModel.Users, u => u.Id == user.Id && u.Username == user.Username);
            }
        }


    }
};
