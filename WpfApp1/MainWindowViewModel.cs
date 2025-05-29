
using System.Collections.ObjectModel;
using System.Windows.Input;
using PublicApi.Api;
using System;
using System.Threading.Tasks;
using WpfApp1;
using PublicAPI.Api;

namespace WpfApp1
{
    public class MainWindowViewModel : NotifyPropertyChangedBase
    {
        public ObservableCollection<User> Users { get; private set; }
        public ICommand NewCommand { get; private set; }
        public ICommand SaveCommand { get; private set; }
        public ICommand DeleteCommand { get; private set; }
        public Predicate<User> ConfirmDelete { get; set; }
        public Action<string> OnError { get; set; }

        private readonly IApiClient _apiClient;

        private User _selectedUser;
        public User SelectedUser
        {
            get { return _selectedUser; }
            set
            {
                _selectedUser = value;
                NotifyPropertyChanged();
            }
        }

        public MainWindowViewModel() : this(new ApiClient())
        {
            // Initializes and loads user data
        }

        public MainWindowViewModel(IApiClient apiClient)
        {
            _apiClient = apiClient;
            Users = new ObservableCollection<User>();

            NewCommand = new RelayCommand<User>(
                // Execute: Create a new User instance
                user =>
                {
                    SelectedUser = new User();
                }
            );

            SaveCommand = new RelayCommand<User>(
                // Execute: Save the selected User
                async user =>
                {
                    try
                    {
                        if (SelectedUser.Id == 0)
                        {
                            await _apiClient.Save(SelectedUser);  // Create new user
                        }
                        else
                        {
                            await _apiClient.Save(SelectedUser);  // Update existing user
                        }
                        await LoadUsers();  // Reload data after save
                    }
                    catch (Exception ex)
                    {
                        OnError?.Invoke($"Error while saving user: {ex.Message}");
                    }
                },
                // CanExecute: Enable the command if SelectedUser is not null
                user => SelectedUser != null
            );

            DeleteCommand = new RelayCommand<User>(
                // Execute: Delete the selected User
                async user =>
                {
                    try
                    {
                        if (ConfirmDelete?.Invoke(SelectedUser) ?? true)
                        {
                            await _apiClient.Delete(SelectedUser.Id);  // Delete user by ID
                            Users.Remove(SelectedUser);
                            SelectedUser = null;
                        }
                    }
                    catch (Exception ex)
                    {
                        OnError?.Invoke($"Error while deleting user: {ex.Message}");
                    }
                },
                // CanExecute: Enable the command if SelectedUser is not null
                user => SelectedUser != null
            );
        }

        // ViewModel method
        public async Task LoadUsers()
        {
            Users.Clear();

            var result = await _apiClient.List<User>();

            if (result == null || result.Value == null)
            {
                OnError?.Invoke("Failed to load users. The response was null.");
                return;
            }

            if (!string.IsNullOrEmpty(result.Error))
            {
                OnError?.Invoke($"Error from server: {result.Error}");
                return;
            }

            foreach (var user in result.Value)
            {
                Users.Add(user);
            }
        }

    }
}
