using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Windows.Forms;
using PublicApi.Api;

namespace KooliProjekt.WinFormsApp
{
    public partial class Form1 : Form, IUserView
    {
        private readonly BindingSource _bindingSource = new BindingSource();

        public IList<User> Users
        {
            get => (IList<User>)_bindingSource.DataSource;
            set => _bindingSource.DataSource = value;
        }

        public User SelectedItem { get; set; }

        public UserPresenter Presenter { get; set; }

        public int Id
        {
            get => int.TryParse(IdField.Text, out var id) ? id : 0;
            set => IdField.Text = value.ToString();
        }

        public string UserEmail
        {
            get => UserEmailField.Text;
            set => UserEmailField.Text = value;
        }

        public string Username
        {
            get => UsernameField.Text;
            set => UsernameField.Text = value;
        }

        public bool IsAdmin
        {
            get => IsAdminCheckbox.Checked;
            set => IsAdminCheckbox.Checked = value;
        }

        public Form1()
        {
            InitializeComponent();

            Presenter = new UserPresenter(this, new ApiClient());

            UsersGrid.AutoGenerateColumns = true;
            UsersGrid.DataSource = _bindingSource;

            UsersGrid.SelectionChanged += UsersGrid_SelectionChanged;

            NewButton.Click += NewButton_Click;
            SaveButton.Click += SaveButton_Click;
            DeleteButton.Click += DeleteButton_Click;

            Load += Form1_Load;
        }

        private async void Form1_Load(object? sender, EventArgs e)
        {
            await Presenter.Load();
        }

        private void UsersGrid_SelectionChanged(object? sender, EventArgs e)
        {
            if (UsersGrid.SelectedRows.Count == 0)
            {
                SelectedItem = null;
            }
            else
            {
                SelectedItem = UsersGrid.SelectedRows[0].DataBoundItem as User;
            }

            Presenter.UpdateView(SelectedItem);
        }

        private void NewButton_Click(object? sender, EventArgs e)
        {
            Presenter.AddNew();
        }

        private async void SaveButton_Click(object? sender, EventArgs e)
        {
            await Presenter.Save();
        }

        private async void DeleteButton_Click(object? sender, EventArgs e)
        {
            await Presenter.Delete();
        }

        public void ShowMessage(string message, string caption, MessageBoxButtons buttons, MessageBoxIcon icon)
        {
            MessageBox.Show(message, caption, buttons, icon);
        }

        public bool ConfirmDelete(string message, string caption)
        {
            return MessageBox.Show(message, caption, MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes;
        }

        public void ClearFields()
        {
            Id = 0;
            Username = string.Empty;
            UserEmail = string.Empty;
            IsAdmin = false;
        }
    }

}
