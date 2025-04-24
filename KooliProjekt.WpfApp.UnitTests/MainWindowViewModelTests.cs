using Moq;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using System.Windows.Input;
using WpfApp1;
using WpfApp1.Api;
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
        public void NewCommand_ShouldCreateNewProduct()
        {
            // Arrange
            var initialCount = _viewModel.Lists.Count;

            // Act
            ((RelayCommand<User>)_viewModel.NewCommand).Execute(null);

            // Assert
            Assert.NotNull(_viewModel.SelectedItem);
            Assert.IsType<User>(_viewModel.SelectedItem);
        }

        [Fact]
        public async Task SaveCommand_ShouldSaveProduct()
        {
            // Arrange
            var user = new User { Id = 1, Name = "John doe" };
            _viewModel.SelectedItem = user;

            // Act
            await Task.Run(() => ((RelayCommand<User>)_viewModel.SaveCommand).Execute(null));

            // Assert
            _apiClientMock.Verify(api => api.Save(user), Times.Once);
            _apiClientMock.Verify(api => api.List(), Times.Once);
        }

        [Fact]
        public async Task DeleteCommand_ShouldDeleteProduct()
        {
            // Arrange
            var user = new User { Id = 1, Name = "John Doe" };
            _viewModel.SelectedItem = user;
            _viewModel.Lists.Add(user);

            _viewModel.ConfirmDelete = p => true;

            // Act
            await Task.Run(() => ((RelayCommand<User>)_viewModel.DeleteCommand).Execute(null));

            // Assert
            _apiClientMock.Verify(api => api.Delete(user.Id), Times.Once);
            Assert.DoesNotContain(user, _viewModel.Lists);
            Assert.Null(_viewModel.SelectedItem);
        }

        [Fact]
        public async Task Load_ShouldPopulateLists()
        {
            // Arrange
            var users = new List<User>
            {
                new User { Id = 1, Name = "John doe" },
                new User { Id = 2, Name = "Jane doe" }
            };
            _apiClientMock
                .Setup(api => api.List())
                .ReturnsAsync(new Result<List<User>> { Value = users });

            // Act
            await _viewModel.Load();

            // Assert
            Assert.Equal(users.Count, _viewModel.Lists.Count);
            foreach (var product in users)
            {
                Assert.Contains(product, _viewModel.Lists);
            }
        }
    }
}