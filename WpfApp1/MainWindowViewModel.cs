using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using System.Windows.Input;
using PublicApi.Api;

namespace WpfApp1
{
    public class MainWindowViewModel : INotifyPropertyChanged
    {
        private readonly IApiClient _apiClient;
        private User _selectedUser;

        public MainWindowViewModel(IApiClient apiClient)
        {
            _apiClient = apiClient ?? throw new ArgumentNullException(nameof(apiClient));
            Users = new ObservableCollection<User>();
            NewCommand = new RelayCommand(CreateNewUser);
            SaveCommand = new RelayCommand(SaveUser, CanSaveUser);
            DeleteCommand = new RelayCommand(DeleteUser, CanDeleteUser);


        }

        public ObservableCollection<User> Users { get; }

        public User SelectedUser
        {
            get => _selectedUser;
            set
            {
                if (_selectedUser != value)
                {
                    _selectedUser = value;
                    OnPropertyChanged();
                    ((RelayCommand)SaveCommand).RaiseCanExecuteChanged();
                    ((RelayCommand)DeleteCommand).RaiseCanExecuteChanged();
                }
            }
        }

        public ICommand NewCommand { get; }
        public ICommand SaveCommand { get; }
        public ICommand DeleteCommand { get; }

        public Func<User, bool> ConfirmDelete { get; set; }
        public Action<string> OnError { get; set; }

        public async Task LoadUsers()
        {
            try
            {
                var result = await _apiClient.List();
                if (result.HasErrors)
                {
                    OnError?.Invoke("Failed to load users: " + string.Join(", ", result.Errors));
                    return;
                }

                Users.Clear();
                foreach (var user in result.Value)
                    Users.Add(user);
            }
            catch (Exception ex)
            {
                OnError?.Invoke($"Exception loading users: {ex.Message}");
            }
        }

        private void CreateNewUser()
        {
            SelectedUser = new User();
        }

        private async void SaveUser()
        {
            if (SelectedUser == null) return;

            try
            {
                var result = await _apiClient.Save(SelectedUser);
                if (result.HasErrors)
                {
                    OnError?.Invoke("Save failed: " + string.Join(", ", result.Errors));
                    return;
                }

                await LoadUsers();
                SelectedUser = result.Value; // Refresh selection with saved user
            }
            catch (Exception ex)
            {
                OnError?.Invoke($"Failed to save user: {ex.Message}");
            }
        }

        private bool CanSaveUser()
        {
            return SelectedUser != null
                && !string.IsNullOrWhiteSpace(SelectedUser.Username)
                && !string.IsNullOrWhiteSpace(SelectedUser.UserEmail);
        }

        private async void DeleteUser()
        {
            if (SelectedUser == null || SelectedUser.Id == 0) return;

            if (ConfirmDelete?.Invoke(SelectedUser) != true)
                return;

            try
            {
                var result = await _apiClient.Delete(SelectedUser.Id);
                if (result.HasErrors)
                {
                    OnError?.Invoke("Delete failed: " + string.Join(", ", result.Errors));
                    return;
                }

                Users.Remove(SelectedUser);
                SelectedUser = null;
            }
            catch (Exception ex)
            {
                OnError?.Invoke($"Failed to delete user: {ex.Message}");
            }
        }

        private bool CanDeleteUser()
        {
            return SelectedUser != null && SelectedUser.Id > 0;
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged([CallerMemberName] string propertyName = null)
            => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}
