
using System.Collections.ObjectModel;
using System.Windows.Input;
using WpfApp1.Api;
using System;
using System.Threading.Tasks;
using WpfApp1;

namespace WpfApp
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
                user =>
                {
                    SelectedUser = new User();
                }
            );

            SaveCommand = new RelayCommand<User>(
                async user =>
                {
                    try
                    {
                        if (SelectedUser.Id == 0)
                        {
                            await _apiClient.Save(SelectedUser);
                        }
                        else
                        {
                            await _apiClient.Save(SelectedUser);
                        }
                        await LoadUsers();
                    }
                    catch (Exception ex)
                    {
                        OnError?.Invoke($"Error while saving user: {ex.Message}");
                    }
                },
                user => SelectedUser != null
            );

            DeleteCommand = new RelayCommand<User>(
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

        public async Task LoadUsers()
        {
            Users.Clear();

            try
            {
                // Call List() without type argument
                var users = await _apiClient.List();  // Assuming List() returns a collection of User objects
                if (users == null)
                {
                    OnError?.Invoke("Failed to load users. The response was null.");
                    return;
                }

                foreach (var user in users)
                {
                    Users.Add(user);  // Add each user to the ObservableCollection
                }
            }
            catch (Exception ex)
            {
                OnError?.Invoke($"Error while loading users: {ex.Message}");
            }
        }
    }
}
