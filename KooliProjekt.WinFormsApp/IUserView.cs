using PublicApi.Api;
using System.Collections.Generic;
using System.Windows.Forms;

namespace KooliProjekt.WinFormsApp
{
    public interface IUserView
    {
        UserPresenter Presenter { get; set; }

        IList<User> Users { get; set; }
        User SelectedItem { get; }

        int Id { get; set; }
        string Username { get; set; }
        string UserEmail { get; set; }
        bool IsAdmin { get; set; }

        void ShowMessage(string message, string caption, MessageBoxButtons buttons, MessageBoxIcon icon);
        bool ConfirmDelete(string message, string caption);
        void ClearFields();
    }
}
