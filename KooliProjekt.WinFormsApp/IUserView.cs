using System.Collections.Generic;
using System.Windows.Forms;
using KooliProjekt.Data;


namespace KooliProjekt.WinFormsApp
{
    public interface IUserView
    {
        IList<User> Users { get; set; }
        User SelectedItem { get; }

        int Id { get; set; }
        int UserId { get; set; }
        string Username { get; set; }
        string UserEmail { get; set; }
        bool IsAdmin { get; set; }

        IUserPresenter Presenter { get; set; }
        void ShowMessage(string message, string caption, MessageBoxButtons buttons, MessageBoxIcon icon);
        bool ConfirmDelete(string message, string caption);
        void ClearFields();
    }
}
