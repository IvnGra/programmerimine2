using KooliProjekt.PublicAPI.Api;

namespace KooliProjekt.WinFormsApp;

public interface IUserView
{
    UserPresenter Presenter { get; set; }

    IList<User> Users { get; set; }  // Use local User here
    User SelectedItem { get; }

    int Id { get; set; }
    string Username { get; set; }
    string UserEmail { get; set; }
    bool IsAdmin { get; set; }

    // Add these methods to match calls in presenter
    void ShowMessage(string message, string caption, MessageBoxButtons buttons, MessageBoxIcon icon);
    bool ConfirmDelete(string message, string caption);
    void ClearFields();
}
