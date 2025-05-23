using KooliProjekt.WinFormsApp.Api;
using Moq;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Windows.Forms;
using Xunit;

namespace KooliProjekt.WinFormsApp.Tests
{
    public class UserPresenterTests
    {
        private readonly Mock<IUserView> _mockView;
        private readonly Mock<IApiClient> _mockApiClient;
        private readonly UserPresenter _presenter;

        public UserPresenterTests()
        {
            _mockView = new Mock<IUserView>();
            _mockApiClient = new Mock<IApiClient>();
            _presenter = new UserPresenter(_mockView.Object, _mockApiClient.Object);
        }

        private void SetupSuccessfulListResponse()
        {
            var users = new List<User>
            {
                new User { Id = 1, Username = "user1", UserEmail = "user1@example.com" }
            };
            var result = new Result<List<User>> { Value = users };
            _mockApiClient.Setup(x => x.List()).ReturnsAsync(result);
        }

        [Fact]
        public async Task InitializeShouldLoadUsers()
        {
            SetupSuccessfulListResponse();

            await _presenter.Initialize();

            _mockApiClient.Verify(x => x.List(), Times.Once);
            _mockView.VerifySet(x => x.Users = It.IsAny<List<User>>(), Times.Once);
        }

        [Fact]
        public async Task DeleteUser_WhenNoUserSelected_ShouldShowWarning()
        {
            _mockView.SetupGet(x => x.Id).Returns(0);

            await _presenter.DeleteUser();

            _mockView.Verify(x => x.ShowMessage(
                "Please select a user to delete.",
                "Error",
                MessageBoxButtons.OK,
                MessageBoxIcon.Warning), Times.Once);

            _mockApiClient.Verify(x => x.Delete(It.IsAny<int>()), Times.Never);
        }

        [Fact]
        public async Task DeleteUser_WhenUserConfirms_ShouldCallApiAndShowSuccess()
        {
            const int userId = 1;
            _mockView.SetupGet(x => x.Id).Returns(userId);

            // Fixed: Removed the nullable bool cast
            _mockView.Setup(x => x.ConfirmDelete(It.IsAny<string>(), It.IsAny<string>()))
                     .Returns(true);

            _mockApiClient.Setup(x => x.Delete(userId)).ReturnsAsync(new Result<bool>());
            SetupSuccessfulListResponse();

            await _presenter.DeleteUser();

            _mockApiClient.Verify(x => x.Delete(userId), Times.Once);
            _mockApiClient.Verify(x => x.List(), Times.Once);

            _mockView.Verify(x => x.ShowMessage(
                "User deleted successfully.",
                "Success",
                MessageBoxButtons.OK,
                MessageBoxIcon.Information), Times.Once);

            _mockView.Verify(x => x.ClearFields(), Times.Once);
        }

        [Fact]
        public async Task SaveUser_WhenFieldsEmpty_ShouldShowWarning()
        {
            _mockView.SetupGet(x => x.Username).Returns("");
            _mockView.SetupGet(x => x.UserEmail).Returns("");

            await _presenter.SaveUser();

            _mockView.Verify(x => x.ShowMessage(
                "Please fill in username and email.",
                "Error",
                MessageBoxButtons.OK,
                MessageBoxIcon.Warning), Times.Once);

            _mockApiClient.Verify(x => x.Save(It.IsAny<User>()), Times.Never);
        }

        [Fact]
        public async Task SaveUser_WhenValid_ShouldCallApiAndShowSuccess()
        {
            const int userId = 1;
            const string username = "user1";
            const string email = "user1@example.com";

            _mockView.SetupGet(x => x.Id).Returns(userId);
            _mockView.SetupGet(x => x.Username).Returns(username);
            _mockView.SetupGet(x => x.UserEmail).Returns(email);

            _mockApiClient.Setup(x => x.Save(It.Is<User>(u =>
                u.Id == userId &&
                u.Username == username &&
                u.UserEmail == email)))
                .ReturnsAsync(new Result<bool>());
            SetupSuccessfulListResponse();

            await _presenter.SaveUser();

            _mockApiClient.Verify(x => x.Save(It.Is<User>(u =>
                u.Id == userId &&
                u.Username == username &&
                u.UserEmail == email)), Times.Once);

            _mockApiClient.Verify(x => x.List(), Times.Once);

            _mockView.Verify(x => x.ShowMessage(
                "User saved successfully.",
                "Success",
                MessageBoxButtons.OK,
                MessageBoxIcon.Information), Times.Once);
        }

        [Fact]
        public void NewUser_ShouldClearFields()
        {
            _presenter.NewUser();

            _mockView.Verify(x => x.ClearFields(), Times.Once);
        }

        [Fact]
        public void UserSelected_WhenUserSelected_ShouldUpdateView()
        {
            var user = new User { Id = 1, Username = "user1", UserEmail = "user1@example.com" };
            _mockView.SetupGet(x => x.SelectedItem).Returns(user);

            _presenter.UserSelected();

            _mockView.VerifySet(x => x.Id = user.Id, Times.Once);
            _mockView.VerifySet(x => x.Username = user.Username, Times.Once);
            _mockView.VerifySet(x => x.UserEmail = user.UserEmail, Times.Once);
        }

        [Fact]
        public void UserSelected_WhenNoUserSelected_ShouldClearFields()
        {
            _mockView.SetupGet(x => x.SelectedItem).Returns((User)null);

            _presenter.UserSelected();

            _mockView.Verify(x => x.ClearFields(), Times.Once);
        }
    }
}