using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using PublicApi.Api;  // User and ApiClient live here

namespace KooliProjekt.WinFormsApp
{
    public class UserPresenter : IUserPresenter
    {
        private readonly IUserView _view;
        private readonly ApiClient _apiClient;

        public UserPresenter(IUserView view, ApiClient apiClient)
        {
            _view = view;
            _apiClient = apiClient;
            _view.Presenter = this;
        }

        public async Task Initialize()
        {
            var result = await _apiClient.List();

            if (result.HasErrors)
            {
                _view.ShowMessage(
                    "Failed to load users: " + string.Join(", ", result.Errors.Select(e => e.Message)),
                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                _view.Users = new List<User>();
            }
            else
            {
                _view.Users = result.Value;
            }
            _view.ClearFields();
        }

        public async Task SaveUser()
        {
            var user = new User
            {
                Id = _view.Id,
                UserNumber = _view.UserId,
                Name = _view.Username,
                Email = _view.UserEmail,
                Admin = _view.IsAdmin
            };

            var result = await _apiClient.Save(user);

            if (result.HasErrors)
            {
                _view.ShowMessage(
                    "Failed to save user: " + string.Join(", ", result.Errors.Select(e => e.Message)),
                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                _view.ClearFields();
                await Initialize();
            }
        }

        public async Task DeleteUser()
        {
            if (_view.SelectedItem == null)
            {
                _view.ShowMessage("No user selected", "Delete User", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (_view.ConfirmDelete("Are you sure you want to delete the selected user?", "Confirm Delete"))
            {
                var result = await _apiClient.Delete(_view.SelectedItem.Id);

                if (result.HasErrors)
                {
                    _view.ShowMessage(
                        "Failed to delete user: " + string.Join(", ", result.Errors.Select(e => e.Message)),
                        "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else
                {
                    _view.ClearFields();
                    await Initialize();
                }
            }
        }

        public void NewUser()
        {
            _view.ClearFields();
        }

        public void UserSelected()
        {
            if (_view.SelectedItem != null)
            {
                var user = _view.SelectedItem;
                _view.Id = user.Id;
                _view.UserId = user.UserNumber;
                _view.Username = user.Name;
                _view.UserEmail = user.Email;
                _view.IsAdmin = user.Admin;
            }
        }
    }
}
