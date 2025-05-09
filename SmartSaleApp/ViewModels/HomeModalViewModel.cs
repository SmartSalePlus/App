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
    public event Action<InvoiceDetailViewModel, bool>? Saved;
    public int Number => _invoiceDetailViewModel.Number;
    public ICommand SaveCommand { get; }
    public ObservableCollection<ProductViewModel> ProductViewModels { get; private set; } = [];

    public int? Count {
        get => _invoiceDetailViewModel.Count;
        set {
            if (_invoiceDetailViewModel.Count != value) {
                _invoiceDetailViewModel.Count = value;
                Total = GetTotal();
                OnPropertyChanged();
                ((Command)SaveCommand).ChangeCanExecute();
            }
        }
    }

    public double? Price {
        get => _invoiceDetailViewModel.Price;
        set {
            if (_invoiceDetailViewModel.Price != value) {
                _invoiceDetailViewModel.Price = value;
                Total = GetTotal();
                OnPropertyChanged();
                ((Command)SaveCommand).ChangeCanExecute();
            }
        }
    }

    public double? Total {
        get => _invoiceDetailViewModel.Total;
        set {
            if (_invoiceDetailViewModel.Total != value) {
                _invoiceDetailViewModel.Total = value ?? 0;
                OnPropertyChanged();
            }
        }
    }

    public ProductViewModel? ProductViewModel {
        get => _invoiceDetailViewModel.ProductViewModel;
        set {
            if (value is not null && _invoiceDetailViewModel.ProductViewModel != value) {
                _invoiceDetailViewModel.ProductViewModel = value;
                Price ??= value?.Price;
                Total = GetTotal();
                OnPropertyChanged();
                ((Command)SaveCommand).ChangeCanExecute();
            }
        }
    }

    private readonly IProductApiClient _productApiClient;
    private readonly INavigation _navigation;
    private InvoiceDetailViewModel _invoiceDetailViewModel;
    private readonly bool _isAdd;

    public HomeModalViewModel(
        IProductApiClient productApiClient,
        INavigation navigation,
        InvoiceDetailViewModel invoiceDetailViewModel,
        bool isAdd
    ) {
        _productApiClient = productApiClient;
        _navigation = navigation;
        _invoiceDetailViewModel = invoiceDetailViewModel;
        _isAdd = isAdd;
        _ = GetProductsAsync();
        SaveCommand = new Command(async () => await SaveAsync(), IsValid);
    }

    private async Task GetProductsAsync() {
        var products = await _productApiClient.GetAsync();
        ProductViewModels = new(products.ToViewModel());
        OnPropertyChanged(nameof(ProductViewModels));
        ProductViewModel = ProductViewModels.FirstOrDefault(x => x.Id == _invoiceDetailViewModel.ProductViewModel?.Id);
    }

    private async Task SaveAsync() {
        Saved?.Invoke(_invoiceDetailViewModel, _isAdd);
        await _navigation.PopModalAsync();
    }

    private double? GetTotal() {
        var total = ProductViewModel?.CountInPackage * Count * Price;
        return total.HasValue ? Math.Round(total.Value) : total;
    }

    private bool IsValid() {
        return ProductViewModel is not null && Count > 0 && Price > 0;
    }

    private void OnPropertyChanged([CallerMemberName] string prop = "") {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prop));
    }
}