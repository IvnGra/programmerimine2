using System.Collections.Generic;
using System.Threading.Tasks;
using System.Windows.Forms;
using KooliProjekt.WinFormsApp.Api;

namespace KooliProjekt.WinFormsApp
{
    public class UserPresenter
    {
        private readonly IUserView _view;
        private readonly IApiClient _apiClient;

        public UserPresenter(IUserView view, IApiClient apiClient)
        {
            _view = view;
            _apiClient = apiClient;
        }

        public async Task Initialize()
        {
            var result = await _apiClient.List();
            _view.Users = result.Value;
        }

        public async Task SaveUser()
        {
            if (string.IsNullOrWhiteSpace(_view.Username) || string.IsNullOrWhiteSpace(_view.UserEmail))
            {
                _view.ShowMessage(
                    "Please fill in username and email.",
                    "Error",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);
                return;
            }

            var user = new User
            {
                Id = _view.Id,
                Username = _view.Username,
                UserEmail = _view.UserEmail
            };

            await _apiClient.Save(user);

            // 🔧 Fix: Refresh the user list and update the view
            var listResult = await _apiClient.List();
            _view.Users = listResult.Value;

            _view.ShowMessage(
                "User saved successfully.",
                "Success",
                MessageBoxButtons.OK,
                MessageBoxIcon.Information);
        }

        public async Task DeleteUser()
        {
            int userId = _view.Id;

            if (userId == 0)
            {
                _view.ShowMessage(
                    "Please select a user to delete.",
                    "Error",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);
                return;
            }

            bool confirm = _view.ConfirmDelete(
                "Are you sure you want to delete this user?",
                "Confirm Delete");

            if (!confirm)
                return;

            await _apiClient.Delete(userId);

            var result = await _apiClient.List();
            _view.Users = result.Value;

            _view.ShowMessage(
                "Users deleted successfully.",
                "Success",
                MessageBoxButtons.OK,
                MessageBoxIcon.Information);

            _view.ClearFields();
        }

        public void NewUser()
        {
            _view.ClearFields();
        }

        public void UserSelected()
        {
            var selectedUser = _view.SelectedItem;

            if (selectedUser == null)
            {
                _view.ClearFields();
                return;
            }

            _view.Id = selectedUser.Id;
            _view.Username = selectedUser.Username;
            _view.UserEmail = selectedUser.UserEmail;
        }
    }
}
