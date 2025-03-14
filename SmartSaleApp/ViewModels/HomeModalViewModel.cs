using SmartSaleApp.Interfaces.ApiClients;
using SmartSaleApp.Models;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Input;

namespace SmartSaleApp.ViewModels;

public sealed class HomeModalViewModel : INotifyPropertyChanged {
    public event PropertyChangedEventHandler? PropertyChanged;
    public int Number { get; }
    public ICommand SaveCommand { get; }
    public ICommand CloseCommand { get; }

    public ObservableCollection<Product> Products {
        get => _products;
        set {
            if (_products != value) {
                _products = value;
                OnPropertyChanged();
            }
        }
    }

    public int? Count {
        get => _invoiceDetail.Count;
        set {
            if (_invoiceDetail.Count != value) {
                _invoiceDetail.Count = value;
                Total = GetTotal();
                OnPropertyChanged();
                ((Command)SaveCommand).ChangeCanExecute();
            }
        }
    }

    public double? Price {
        get => _invoiceDetail.Price;
        set {
            if (_invoiceDetail.Price != value) {
                _invoiceDetail.Price = value;
                Total = GetTotal();
                OnPropertyChanged();
                ((Command)SaveCommand).ChangeCanExecute();
            }
        }
    }


    public Product? Product {
        get => _invoiceDetail.Product;
        set {
            if (_invoiceDetail.Product != value) {
                _invoiceDetail.Product = value;
                Price ??= value?.Price;
                Total = GetTotal();
                OnPropertyChanged();
                ((Command)SaveCommand).ChangeCanExecute();
            }
        }
    }

    public double? Total {
        get => _invoiceDetail.Total;
        set {
            if (_invoiceDetail.Total != value) {
                _invoiceDetail.Total = value;
                OnPropertyChanged();
            }
        }
    }

    private readonly IProductApiClient _productApiClient;
    private readonly INavigation _navigation;
    private readonly Action<InvoiceDetail> _invoiceDetailAddedHandler;
    private readonly InvoiceDetail _invoiceDetail;
    private ObservableCollection<Product> _products = [];

    public HomeModalViewModel(
        IProductApiClient productApiClient,
        INavigation navigation,
        Action<InvoiceDetail> invoiceDetailAddedHandler, 
        int number
    ) {
        _productApiClient = productApiClient;
        _navigation = navigation;
        _invoiceDetailAddedHandler = invoiceDetailAddedHandler;
        Number = number;
        _invoiceDetail = new();
        _ = GetProductsAsync();
        SaveCommand = new Command(async () => await SaveAsync(), IsValid);
        CloseCommand = new Command(async () => await CloseAsync());
    }

    private async Task GetProductsAsync() {
        var products = await _productApiClient.GetAsync();
        Products = new(products);
    }

    private async Task SaveAsync() {
        _invoiceDetailAddedHandler?.Invoke(_invoiceDetail);
        var t = Application.Current?.MainPage;
        if (t != null) {
            await t.Navigation.PopModalAsync();
        }
    }
    
    private async Task CloseAsync() {
        await _navigation.PopModalAsync();
    }

    private double? GetTotal() {
        var total = Product?.CountInPackage * Count * Price;
        return total.HasValue ? Math.Round(total.Value) : total;
    }

    private bool IsValid() {
        return Product is not null && Count > 0 && Price > 0;
    }

    private void OnPropertyChanged([CallerMemberName] string prop = "") {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prop));
    }
}