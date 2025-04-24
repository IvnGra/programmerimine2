using KooliProjekt.WpfApp.Api;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using System.Windows.Input;
using System.Xml.Linq;
using WpfApp1;
using WpfApp1.Api;

namespace WpfApp1
{
    public class MainWindowViewModel : NotifyPropertyChangedBase
    {
        public ObservableCollection<User> Lists { get; private set; }
        public ICommand NewCommand { get; private set; }
        public ICommand SaveCommand { get; private set; }
        public ICommand DeleteCommand { get; private set; }
        public Predicate<User> ConfirmDelete { get; set; }

        private readonly IApiClient _apiClient;

        public MainWindowViewModel() : this(new ApiClient())
        {
        }

        public MainWindowViewModel(IApiClient apiClient)
        {
            _apiClient = apiClient;

            Lists = new ObservableCollection<User>();

            NewCommand = new RelayCommand<User>(
                // Execute
                list =>
                {
                    SelectedItem = new User();
                }
            );

            SaveCommand = new RelayCommand<User>(
                // Execute
                async list =>
                {
                    await _apiClient.Save(SelectedItem);
                    await Load();
                },
                // CanExecute
                list =>
                {
                    return SelectedItem != null;
                }
            );

            DeleteCommand = new RelayCommand<User>(
                // Execute
                async list =>
                {
                    if (ConfirmDelete != null)
                    {
                        var result = ConfirmDelete(SelectedItem);
                        if (!result)
                        {
                            return;
                        }
                    }

                    await _apiClient.Delete(SelectedItem.Id);
                    Lists.Remove(SelectedItem);
                    SelectedItem = null;
                },
                // CanExecute
                list =>
                {
                    return SelectedItem != null;
                }
            );
        }

        public async Task Load()
        {
            Lists.Clear();

            var result = await _apiClient.List();
            if (result.HasError)
            {
                // Handle error
                return;
            }

            foreach (var list in result.Value)
            {
                Lists.Add(list);
            }
        }

        private User _selectedItem;
        public User SelectedItem
        {
            get
            {
                return _selectedItem;
            }
            set
            {
                _selectedItem = value;
                NotifyPropertyChanged();
            }
        }
    }
}