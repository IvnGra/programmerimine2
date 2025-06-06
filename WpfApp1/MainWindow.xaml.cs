using System;
using System.Windows;
using PublicApi.Api;

namespace WpfApp1
{
    public partial class MainWindow : Window
    {
        private readonly MainWindowViewModel _viewModel;

        public MainWindow()
        {
            InitializeComponent();

            // Instantiate API client and ViewModel
            var apiClient = new ApiClient();
            _viewModel = new MainWindowViewModel(apiClient);

            // Set up confirm and error dialogs
            _viewModel.ConfirmDelete = user =>
            {
                var result = MessageBox.Show(
                    $"Are you sure you want to delete user '{user.Username}'?",
                    "Confirm Delete",
                    MessageBoxButton.YesNo,
                    MessageBoxImage.Warning);
                return result == MessageBoxResult.Yes;
            };

            _viewModel.OnError = error =>
            {
                MessageBox.Show(error, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            };

            DataContext = _viewModel;

            Loaded += MainWindow_Loaded;
        }

        private async void MainWindow_Loaded(object sender, RoutedEventArgs e)
        {
            await _viewModel.LoadUsers();
        }
    }
}
