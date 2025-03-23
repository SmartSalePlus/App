using SmartSaleApp.Dto;
using SmartSaleApp.Extensions;
using SmartSaleApp.Interfaces.ApiClients;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Input;

namespace SmartSaleApp.ViewModels;

public sealed class HomeModalViewModel : INotifyPropertyChanged {
    public event PropertyChangedEventHandler? PropertyChanged;
    public int Number => _invoiceDetail.Number;
    public ICommand SaveCommand { get; }
    public ICommand CloseCommand { get; }
    public ObservableCollection<ProductDto> Products { get; private set; } = [];

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


    public ProductDto? Product {
        get => _invoiceDetail.ProductDto;
        set {
            if (value is not null && _invoiceDetail.ProductDto != value) {
                _invoiceDetail.ProductDto = value;
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
    private readonly Action<InvoiceDetailDto, bool> _invoiceDetailAddedHandler;
    private readonly InvoiceDetailDto _invoiceDetail;
    private readonly bool _isAdd;

    public HomeModalViewModel(
        IProductApiClient productApiClient,
        INavigation navigation,
        Action<InvoiceDetailDto, bool> invoiceDetailAddedHandler,
        InvoiceDetailDto invoiceDetailDto,
        bool isAdd
    ) {
        _productApiClient = productApiClient;
        _navigation = navigation;
        _invoiceDetailAddedHandler = invoiceDetailAddedHandler;
        _invoiceDetail = invoiceDetailDto;
        _isAdd = isAdd;
        _ = GetProductsAsync();
        SaveCommand = new Command(async () => await SaveAsync(), IsValid);
        CloseCommand = new Command(async () => await CloseAsync());
    }

    private async Task GetProductsAsync() {
        var products = await _productApiClient.GetAsync();
        Products = new(products.ToDto());
        OnPropertyChanged(nameof(Products));
        Product = Products.FirstOrDefault(x => x.Id == _invoiceDetail.ProductDto?.Id);
    }

    private async Task SaveAsync() {
        _invoiceDetailAddedHandler?.Invoke(_invoiceDetail, _isAdd);
        await _navigation.PopModalAsync();
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