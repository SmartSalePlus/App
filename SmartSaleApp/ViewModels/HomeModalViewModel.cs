using SmartSaleApp.Interfaces.ApiClients;
using SmartSaleApp.Models;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Input;

namespace SmartSaleApp.ViewModels;

public sealed partial class HomeModalViewModel : INotifyPropertyChanged {
    public event PropertyChangedEventHandler? PropertyChanged;
    public ICommand SaveCommand { get; }
    public ICommand CloseCommand { get; }

    public ObservableCollection<Product> Products {
        get => _products;
        set {
            _products = value;
            OnPropertyChanged();
        }
    }

    public int? Count {
        get => _invoiceDetail.Count;
        set {
            _invoiceDetail.Count = value;
            Total = GetTotal();
            OnPropertyChanged();
            ((Command)SaveCommand).ChangeCanExecute();

        }
    }

    public double? Price {
        get => _invoiceDetail.Price;
        set {
            _invoiceDetail.Price = value;
            Total = GetTotal();
            OnPropertyChanged();
            ((Command)SaveCommand).ChangeCanExecute();
        }
    }


    public Product? Product {
        get => _invoiceDetail.Product;
        set {
            _invoiceDetail.Product = value;
            Price ??= value?.Price;
            Total = GetTotal();
            OnPropertyChanged();
            ((Command)SaveCommand).ChangeCanExecute();
        }
    }

    public double? Total {
        get => _invoiceDetail.Total;
        set {
            _invoiceDetail.Total = value;
            OnPropertyChanged();
        }
    }

    public int Number => _number;

    private readonly IProductApiClient _productApiClient;
    public INavigation Navigation { get; set; }
    public ObservableCollection<InvoiceDetail> InvoiceDetails { get; set; }
    //private readonly INavigation _navigation;
    private readonly InvoiceDetail _invoiceDetail;

    private int _number;
    private ObservableCollection<Product> _products = [];

    public HomeModalViewModel(IProductApiClient productApiClient) {
        _productApiClient = productApiClient;
        //_navigation = navigation;
        _number = 1;
        _invoiceDetail = new();
        _ = GetProducts();
        SaveCommand = new Command(Save, IsValid);
        CloseCommand = new Command(Close);
    }

    private async Task GetProducts() {
        var products = await _productApiClient.GetAsync();
        Products = new(products);
    }

    private void Save() {
        InvoiceDetails.Add(_invoiceDetail);
        _number++;
        OnPropertyChanged(nameof(Number));
    }
    
    private async void Close() {
        await Navigation.PopModalAsync();
    }

    private double? GetTotal() {
        var total = Product?.CountInPackage * Count * Price;
        if (total.HasValue) {
            return Math.Round(total.Value);
        }
        return total;
    }

    private bool IsValid() {
        return Product is not null && Count > 0 && Price > 0;
    }

    public void OnPropertyChanged([CallerMemberName] string prop = "") {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prop));
    }
}