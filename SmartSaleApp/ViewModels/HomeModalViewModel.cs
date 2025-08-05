using SmartSaleApp.Extensions.Mapping;
using SmartSaleApp.Interfaces.ApiClients;
using SmartSaleApp.Models.View;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Input;

namespace SmartSaleApp.ViewModels;

public sealed class HomeModalViewModel : INotifyPropertyChanged {
    public event PropertyChangedEventHandler? PropertyChanged;
    public event Action<InvoiceDetailDto, bool>? Saved;
    public event Action? Removed;
    public ICommand SaveCommand { get; }
    public ICommand RemoveCommand { get; }
    public ObservableCollection<ProductDto> ProductDtos { get; private set; } = [];
    public string Title => _isAdd ? "Добавление товара" : "Редактирование товара";
    public bool IsVisibleRemoveButton => !_isAdd;

    public int? Count {
        get => _invoiceDetailDto.Count;
        set {
            if (_invoiceDetailDto.Count != value) {
                _invoiceDetailDto.Count = value;
                Total = GetTotal();
                OnPropertyChanged();
                ((Command)SaveCommand).ChangeCanExecute();
            }
        }
    }

    public double? Price {
        get => _invoiceDetailDto.Price;
        set {
            if (_invoiceDetailDto.Price != value) {
                _invoiceDetailDto.Price = value;
                Total = GetTotal();
                OnPropertyChanged();
                ((Command)SaveCommand).ChangeCanExecute();
            }
        }
    }

    public double? Total {
        get => _invoiceDetailDto.Total;
        set {
            if (_invoiceDetailDto.Total != value) {
                _invoiceDetailDto.Total = value ?? 0;
                OnPropertyChanged();
            }
        }
    }

    public ProductDto? ProductDto {
        get => _invoiceDetailDto.ProductDto;
        set {
            if (value is not null && _invoiceDetailDto.ProductDto != value) {
                _invoiceDetailDto.ProductDto = value;
                Price ??= value?.Price;
                Total = GetTotal();
                OnPropertyChanged();
                ((Command)SaveCommand).ChangeCanExecute();
            }
        }
    }

    private readonly IProductApiClient _productApiClient;
    private readonly INavigation _navigation;
    private InvoiceDetailDto _invoiceDetailDto;
    private readonly bool _isAdd;

    public HomeModalViewModel(
        IProductApiClient productApiClient,
        INavigation navigation,
        InvoiceDetailDto invoiceDetailDto,
        bool isAdd
    ) {
        _productApiClient = productApiClient;
        _navigation = navigation;
        _invoiceDetailDto = invoiceDetailDto;
        _isAdd = isAdd;
        _ = GetProductsAsync();
        SaveCommand = new Command(async () => await SaveAsync(), IsValid);
        RemoveCommand = new Command(async () => await Remove());
    }

    private async Task GetProductsAsync() {
        var products = await _productApiClient.GetAsync();
        ProductDtos = new(products.ToDto());
        OnPropertyChanged(nameof(ProductDtos));
        ProductDto = ProductDtos.FirstOrDefault(x => x.Id == _invoiceDetailDto.ProductDto?.Id);
    }

    private async Task SaveAsync() {
        Saved?.Invoke(_invoiceDetailDto, _isAdd);
        await _navigation.PopModalAsync();
    }

    private async Task Remove() {
        Removed?.Invoke();
        await _navigation.PopModalAsync();
    }

    private double? GetTotal() {
        var total = ProductDto?.CountInPackage * Count * Price;
        return total.HasValue ? Math.Round(total.Value) : total;
    }

    private bool IsValid() {
        return ProductDto != null && Count > 0 && Price > 0;
    }

    private void OnPropertyChanged([CallerMemberName] string prop = "") {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prop));
    }
}