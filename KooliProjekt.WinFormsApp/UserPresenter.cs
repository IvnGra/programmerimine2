using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using PublicApi.Api;

namespace KooliProjekt.WinFormsApp
{
    public class UserPresenter : IUserPresenter
    {
        private readonly IApiClient _apiClient;
        private readonly IUserView _view;

        public UserPresenter(IUserView view, IApiClient apiClient)
        {
            _view = view;
            _apiClient = apiClient;
        }

        public async Task Initialize()
        {
            await LoadUsers();
        }

        private async Task LoadUsers()
        {
            var response = await _apiClient.List();
            if (response.HasError)
            {
                _view.ShowMessage($"Error loading users: {response.Error}", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            _view.Users = (IList<Data.User>)response.Value;
        }

        public async Task DeleteUser()
        {
            if (_view.Id == 0)
            {
                _view.ShowMessage("Please select a user to delete.", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (_view.ConfirmDelete("Are you sure you want to delete this user?", "Confirm Delete"))
            {
                var result = await _apiClient.Delete(_view.Id);
                if (result.HasError)
                {
                    _view.ShowMessage($"Error deleting user: {result.Error}", "Error",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else
                {
                    _view.ShowMessage("Users deleted successfully.", "Success",
                        MessageBoxButtons.OK, MessageBoxIcon.Information);
                    await LoadUsers();
                    _view.ClearFields();
                }
            }
        }

        public async Task SaveUser()
        {
            if (string.IsNullOrWhiteSpace(_view.Username) || string.IsNullOrWhiteSpace(_view.UserEmail))
            {
                _view.ShowMessage("Please fill in username and email.", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            var user = new User
            {
                Id = _view.Id,
                UserId = _view.UserId,
                Username = _view.Username,
                UserEmail = _view.UserEmail,
                IsAdmin = _view.IsAdmin
            };

            var result = await _apiClient.Save(user);

            if (!result.HasError)
            {
                _view.ShowMessage($"Error saving user: {result.Error}", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                _view.ShowMessage("User saved successfully.", "Success",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                await LoadUsers();
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
                var selected = _view.SelectedItem;
                _view.Id = selected.Id;
                _view.UserId = selected.UserId;
                _view.Username = selected.Username;
                _view.UserEmail = selected.UserEmail;
                _view.IsAdmin = selected.IsAdmin;
            }
            else
            {
                _view.ClearFields();
            }
        }
    }
}
