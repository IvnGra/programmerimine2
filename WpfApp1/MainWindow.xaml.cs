using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;
using System.Net.Http;
using System.Net.Http.Json;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace WpfApp1
{
    public partial class Product : INotifyPropertyChanged
    {
        private int _id;
        private string _name;
        private decimal _price;

        public int Id { get => _id; set { _id = value; OnPropertyChanged(); } }
        public string Name { get => _name; set { _name = value; OnPropertyChanged(); } }
        public decimal Price { get => _price; set { _price = value; OnPropertyChanged(); } }

        public event PropertyChangedEventHandler PropertyChanged;
        private void OnPropertyChanged([CallerMemberName] string propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }

    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void ShowCode_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                // Path to the XAML.cs file
                string filePath = "MainWindow.xaml.cs"; // Adjust as needed
                if (File.Exists(filePath))
                {
                    // Read and display the file content
                    CodeViewer.Text = File.ReadAllText(filePath);
                }
                else
                {
                    CodeViewer.Text = "File not found.";
                }
            }
            catch (Exception ex)
            {
                CodeViewer.Text = "Error: " + ex.Message;
            }
        }
    }

    public class ProductService
    {
        private readonly HttpClient _httpClient = new HttpClient { BaseAddress = new Uri("http://localhost:5000/api/") };

        public async Task GetProductsAsync(Product product)
        {
            await _httpClient.GetFromJsonAsync<ObservableCollection<Product>>("ProductApi");
        }

        public async Task AddProductAsync(Product product)
        {
            await _httpClient.PostAsJsonAsync("ProductApi", product);
        }

        public async Task UpdateProductAsync(Product product)
        {
            await _httpClient.PutAsJsonAsync($"ProductApi/{product.Id}", product);
        }

        public async Task DeleteProductAsync(int id)
        {
            await _httpClient.DeleteAsync($"ProductApi/{id}");
        }

        internal async Task<Product> GetProductsAsync()
        {
            throw new NotImplementedException();
        }
    }

    public class ProductViewModel : INotifyPropertyChanged
    {
        private readonly ProductService _service = new ProductService();
        public Product Products { get; set; } = new();
        private Product _selectedProduct;
        public Product SelectedProduct { get => _selectedProduct; set { _selectedProduct = value; OnPropertyChanged(); } }

        public ICommand LoadCommand => new RelayCommand(async _ => await LoadProducts());
        public ICommand AddCommand => new RelayCommand(async _ => await AddProduct());
        public ICommand UpdateCommand => new RelayCommand(async _ => await UpdateProduct(), _ => SelectedProduct != null);
        public ICommand DeleteCommand => new RelayCommand(async _ => await DeleteProduct(), _ => SelectedProduct != null);

        public event PropertyChangedEventHandler PropertyChanged;
        private void OnPropertyChanged([CallerMemberName] string propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        private async Task LoadProducts()
        {
            Products = await _service.GetProductsAsync();
            OnPropertyChanged(nameof(Products));
        }

        private async Task AddProduct()
        {
            var newProduct = new Product { Name = "New Product", Price = 0 };
            await _service.AddProductAsync(newProduct);
            await LoadProducts();
        }

        private async Task UpdateProduct()
        {
            if (SelectedProduct != null)
            {
                await _service.UpdateProductAsync(SelectedProduct);
                await LoadProducts();
            }
        }

        private async Task DeleteProduct()
        {
            if (SelectedProduct != null)
            {
                await _service.DeleteProductAsync(SelectedProduct.Id);
                await LoadProducts();
            }
        }
    }

    public class RelayCommand : ICommand
    {
        private readonly Func<object, Task> _execute;
        private readonly Func<object, bool> _canExecute;

        public RelayCommand(Func<object, Task> execute, Func<object, bool> canExecute = null)
        {
            _execute = execute;
            _canExecute = canExecute;
        }

        public bool CanExecute(object parameter) => _canExecute == null || _canExecute(parameter);
        public async void Execute(object parameter) => await _execute(parameter);
        public event EventHandler CanExecuteChanged { add { } remove { } }
    }
}