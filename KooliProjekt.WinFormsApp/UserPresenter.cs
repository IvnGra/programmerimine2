using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Windows.Forms;
using KooliProjekt.PublicAPI.Api;

namespace KooliProjekt.WinFormsApp
{
    public class UserPresenter
    {
        private readonly IApiClient _apiClient;
        private readonly IUserView _userView;

        public UserPresenter(IUserView userView, IApiClient apiClient)
        {
            _apiClient = apiClient;
            _userView = userView;
            _userView.Presenter = this;
        }

        public void UpdateView(User? user)
        {
            if (user == null)
            {
                _userView.Id = 0;
                _userView.Username = string.Empty;
                _userView.UserEmail = string.Empty;
                _userView.IsAdmin = false;
            }
            else
            {
                _userView.Id = user.Id;
                _userView.Username = user.Username;
                _userView.UserEmail = user.UserEmail;
                _userView.IsAdmin = user.IsAdmin;
            }
        }

        public async Task Load()
        {
            var result = await _apiClient.List();

            if (result.IsSuccess)
            {
                _userView.Users = result.Value ?? new List<User>();
            }
            else
            {
                _userView.ShowMessage(result.Error ?? "Failed to load users.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        public void AddNew()
        {
            _userView.ClearFields();
        }

        public async Task Save()
        {
            var user = new User
            {
                Id = _userView.Id,
                Username = _userView.Username,
                UserEmail = _userView.UserEmail,
                IsAdmin = _userView.IsAdmin
            };

            var result = await _apiClient.Save(user);
            if (!result.IsSuccess)
            {
                _userView.ShowMessage(result.Error ?? "Failed to save user.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                _userView.ClearFields();
                await Load();
            }
        }

        public async Task Delete()
        {
            if (_userView.SelectedItem == null)
            {
                _userView.ShowMessage("Please select a user to delete.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (_userView.ConfirmDelete("Are you sure you want to delete the selected user?", "Confirm Delete"))
            {
                var result = await _apiClient.Delete(_userView.SelectedItem.Id);
                if (!result.IsSuccess)
                {
                    _userView.ShowMessage(result.Error ?? "Failed to delete user.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else
                {
                    _userView.ClearFields();
                    await Load();
                }
            }
        }
    }
}
