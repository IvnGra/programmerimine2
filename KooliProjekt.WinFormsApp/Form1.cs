using PublicApi.Api;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Windows.Forms;
using KooliProjekt.Data;

namespace KooliProjekt.WinFormsApp
{
    public partial class Form1 : Form, IUserView
    {
        private IUserPresenter _presenter;
        private readonly BindingSource _bindingSource = new BindingSource();

        public Form1()
        {
            InitializeComponent();
            Presenter = new UserPresenter(this, new ApiClient(new HttpClient()));
            InitializeDataGridView();
        }

        protected override async void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            await Presenter.Initialize();
        }

        #region IUserView Implementation

        public IUserPresenter Presenter
        {
            get => _presenter;
            set => _presenter = value;
        }

        public IList<User> Users
        {
            get => (IList<User>)_bindingSource.DataSource;
            set => _bindingSource.DataSource = value;
        }

        public User SelectedItem => UsersGrid.SelectedRows.Count > 0
            ? (User)UsersGrid.SelectedRows[0].DataBoundItem
            : null;

        public int Id
        {
            get => int.TryParse(IdField.Text, out int id) ? id : 0;
            set => IdField.Text = value.ToString();
        }

        public int UserId
        {
            get => int.TryParse(UserIdField.Text, out int userId) ? userId : 0;
            set => UserIdField.Text = value.ToString();
        }

        public string Username
        {
            get => UsernameField.Text;
            set => UsernameField.Text = value;
        }

        public string UserEmail
        {
            get => UserEmailField.Text;
            set => UserEmailField.Text = value;
        }

        public bool IsAdmin
        {
            get => IsAdminCheckbox.Checked;
            set => IsAdminCheckbox.Checked = value;
        }

        public void ShowMessage(string message, string caption, MessageBoxButtons buttons, MessageBoxIcon icon)
        {
            MessageBox.Show(this, message, caption, buttons, icon);
        }

        public bool ConfirmDelete(string message, string caption)
        {
            return MessageBox.Show(this, message, caption,
                MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes;
        }

        public void ClearFields()
        {
            IdField.Text = "0";
            UserIdField.Text = "0";
            UsernameField.Text = string.Empty;
            UserEmailField.Text = string.Empty;
            IsAdminCheckbox.Checked = false;
            UsersGrid.ClearSelection();
        }

        #endregion

        #region Event Handlers

        private void UsersGrid_SelectionChanged(object sender, EventArgs e)
        {
            Presenter.UserSelected();
        }

        private async void SaveButton_Click(object sender, EventArgs e)
        {
            await Presenter.SaveUser();
        }

        private async void DeleteButton_Click(object sender, EventArgs e)
        {
            await Presenter.DeleteUser();
        }

        private void NewButton_Click(object sender, EventArgs e)
        {
            Presenter.NewUser();
        }

        #endregion

        private void InitializeDataGridView()
        {
            UsersGrid.AutoGenerateColumns = false;
            UsersGrid.Columns.Clear();

            UsersGrid.Columns.Add(new DataGridViewTextBoxColumn()
            {
                DataPropertyName = "Id",
                HeaderText = "ID",
                Width = 50
            });

            UsersGrid.Columns.Add(new DataGridViewTextBoxColumn()
            {
                DataPropertyName = "UserNumber",
                HeaderText = "User ID",
                Width = 75
            });

            UsersGrid.Columns.Add(new DataGridViewTextBoxColumn()
            {
                DataPropertyName = "Name",
                HeaderText = "Username",
                Width = 150,
                AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill
            });

            UsersGrid.Columns.Add(new DataGridViewTextBoxColumn()
            {
                DataPropertyName = "Email",
                HeaderText = "Email",
                Width = 200
            });

            UsersGrid.Columns.Add(new DataGridViewCheckBoxColumn()
            {
                DataPropertyName = "Admin",
                HeaderText = "Admin",
                Width = 50
            });

            UsersGrid.DataSource = _bindingSource;
        }
    }
}
