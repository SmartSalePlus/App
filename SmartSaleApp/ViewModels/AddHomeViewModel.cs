using SmartSaleApp.Interfaces.ApiClients;
using SmartSaleApp.Models;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace SmartSaleApp.ViewModels;

public sealed class AddHomeViewModel : INotifyPropertyChanged {
    public event PropertyChangedEventHandler? PropertyChanged;

    public ObservableCollection<Product> Products { get; set; } = [];

    public int? Count {
        get => _invoiceDetail.Count;
        set {
            if (_invoiceDetail.Count != value) {
                _invoiceDetail.Count = value;
                OnPropertyChanged();
                OnPropertyChanged(nameof(Total));
            }
        }
    }

    public double? Price {
        get => _invoiceDetail.Price;
        set {
            if (_invoiceDetail.Price != value) {
                _invoiceDetail.Price = value;
                OnPropertyChanged();
                OnPropertyChanged(nameof(Total));
            }
        }
    }

    public double Total => _product?.CountInPackage * Count * Price ?? 0;
    public int Number { get; private set; }

    private readonly IProductApiClient _productApiClient;
    private int _number;
    //private ObservableCollection<Product> _products = [];
    private readonly InvoiceDetail _invoiceDetail;
    private readonly Product? _product;

    public AddHomeViewModel(IProductApiClient productApiClient, int number) {
        _productApiClient = productApiClient;
        Number = number;
        _invoiceDetail = new();
        _ = GetProducts();
    }

    private async Task GetProducts() {
        var products = await _productApiClient.GetAsync();
        Products = new(products);
    }

    private void Save() {
        var invoiceDetail = new InvoiceDetail();
        Number++;
    }

    public void OnPropertyChanged([CallerMemberName] string prop = "") {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prop));
    }
}