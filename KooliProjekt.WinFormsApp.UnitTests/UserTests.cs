using System.Collections.Generic;
using System.Threading.Tasks;
using Moq;
using PublicApi.Api;
using System.Windows.Forms;
using Xunit;
using KooliProjekt.WinFormsApp;

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
                new User { Id = 1, Username = "user1", UserEmail = "user1@example.com", IsAdmin = false }
            };
            var result = new Result<List<User>> { Value = users };
            _mockApiClient.Setup(x => x.List()).ReturnsAsync(result);
        }

        [Fact]
        public async Task Load_ShouldLoadUsers()
        {
            SetupSuccessfulListResponse();

            await _presenter.Load();

            _mockApiClient.Verify(x => x.List(), Times.Once);
            _mockView.VerifySet(x => x.Users = It.IsAny<List<User>>(), Times.Once);
        }

        [Fact]
        public void AddNew_ShouldClearFields()
        {
            _presenter.AddNew();

            _mockView.Verify(x => x.ClearFields(), Times.Once);
        }

        [Fact]
        public async Task Save_WhenValid_ShouldCallApiAndReload()
        {
            _mockView.SetupGet(x => x.Id).Returns(1);
            _mockView.SetupGet(x => x.Username).Returns("user1");
            _mockView.SetupGet(x => x.UserEmail).Returns("user1@example.com");
            _mockView.SetupGet(x => x.IsAdmin).Returns(false);

            _mockApiClient.Setup(x => x.Save(It.Is<User>(u =>
                u.Id == 1 &&
                u.Username == "user1" &&
                u.UserEmail == "user1@example.com" &&
                u.IsAdmin == false)))
                .ReturnsAsync(new Result<User>
                {
                    Value = new User
                    {
                        Id = 1,
                        Username = "user1",
                        UserEmail = "user1@example.com",
                        IsAdmin = false
                    }
                });

            SetupSuccessfulListResponse();

            await _presenter.Save();

            _mockApiClient.Verify(x => x.Save(It.IsAny<User>()), Times.Once);
            _mockApiClient.Verify(x => x.List(), Times.Once);
            _mockView.Verify(x => x.ClearFields(), Times.Once);
        }

        [Fact]
        public async Task Delete_WhenNoUserSelected_ShouldShowWarning()
        {
            _mockView.SetupGet(x => x.SelectedItem).Returns((User)null);

            await _presenter.Delete();

            _mockView.Verify(x => x.ShowMessage(
                "Please select a user to delete.",
                "Warning",
                MessageBoxButtons.OK,
                MessageBoxIcon.Warning), Times.Once);

            _mockApiClient.Verify(x => x.Delete(It.IsAny<int>()), Times.Never);
        }

        [Fact]
        public async Task Delete_WhenConfirmed_ShouldCallApiAndReload()
        {
            var user = new User { Id = 1, Username = "user1", UserEmail = "user1@example.com", IsAdmin = false };
            _mockView.SetupGet(x => x.SelectedItem).Returns(user);
            _mockView.Setup(x => x.ConfirmDelete(It.IsAny<string>(), It.IsAny<string>())).Returns(true);

            _mockApiClient.Setup(x => x.Delete(user.Id)).ReturnsAsync(new Result<bool>());
            SetupSuccessfulListResponse();

            await _presenter.Delete();

            _mockApiClient.Verify(x => x.Delete(user.Id), Times.Once);
            _mockApiClient.Verify(x => x.List(), Times.Once);
            _mockView.Verify(x => x.ClearFields(), Times.Once);
        }

        [Fact]
        public void UpdateView_WhenUserSelected_ShouldSetViewProperties()
        {
            var user = new User { Id = 1, Username = "user1", UserEmail = "user1@example.com", IsAdmin = false };

            _presenter.UpdateView(user);

            _mockView.VerifySet(x => x.Id = user.Id, Times.Once);
            _mockView.VerifySet(x => x.Username = user.Username, Times.Once);
            _mockView.VerifySet(x => x.UserEmail = user.UserEmail, Times.Once);
            _mockView.VerifySet(x => x.IsAdmin = user.IsAdmin, Times.Once);
        }

        [Fact]
        public void UpdateView_WhenNoUserSelected_ShouldClearViewProperties()
        {
            _presenter.UpdateView(null);

            _mockView.VerifySet(x => x.Id = 0, Times.Once);
            _mockView.VerifySet(x => x.Username = "", Times.Once);
            _mockView.VerifySet(x => x.UserEmail = "", Times.Once);
            _mockView.VerifySet(x => x.IsAdmin = false, Times.Once);
        }
    }
}
