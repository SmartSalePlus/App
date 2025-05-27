using SmartSaleApp.Extensions.Mapping;
using SmartSaleApp.Interfaces.ApiClients;
using SmartSaleApp.Interfaces.Factory;
using SmartSaleApp.Models.View;
using SmartSaleApp.Pages;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Input;

namespace SmartSaleApp.ViewModels;

public sealed class ProductViewModel : INotifyPropertyChanged {
    public event PropertyChangedEventHandler? PropertyChanged;
    public ICommand LoadCommand { get; }
    public ICommand AddCommand { get; }
    public ObservableCollection<ProductDto> ProductDtos { get; private set; } = [];
    public DateTime DateTimeLoad { get; private set; }

    public ProductDto? ProductDto {
        get => null;
        set {
            if (value != null) {
                _ = EditAsync(value);
                OnPropertyChanged();
            }
        }
    }

    private readonly IProductModalViewModelFactory _productModalViewModelFactory;
    private readonly IProductApiClient _productApiClient;
    private readonly INavigation _navigation;

    public ProductViewModel(
        IProductModalViewModelFactory productModalViewModelFactory,
        IProductApiClient productApiClient,
        INavigation navigation
    ) {
        _productModalViewModelFactory = productModalViewModelFactory;
        _productApiClient = productApiClient;
        _navigation = navigation;
        OnLoad();
        DateTimeLoad = DateTime.Now;
        LoadCommand = new Command(async () => await GetAsync());
        AddCommand = new Command(async () => await AddAsync());
    }

    private async Task GetAsync() {
        var products = await _productApiClient.GetAsync();
        ProductDtos = new(products.ToDto());
        OnPropertyChanged(nameof(ProductDtos));
        DateTimeLoad = DateTime.Now;
        OnPropertyChanged(nameof(DateTimeLoad));
    }

    private async Task AddAsync() {
        await OpenModalPageAsync(new(), true);
    }

    private async Task EditAsync(ProductDto productDto) {
        var cloneProductDto = productDto.Clone();
        await OpenModalPageAsync(cloneProductDto, false);
    }

    private async Task OpenModalPageAsync(ProductDto productDto, bool isAdd) {
        var productModalViewModel = _productModalViewModelFactory.Create(_navigation, productDto, isAdd);
        productModalViewModel.Saved += OnLoad;
        var homeModalPage = new ProductModalPage(productModalViewModel);
        await _navigation.PushModalAsync(homeModalPage);
    }

    private void OnLoad() {
        _ = GetAsync();
    }

    private void OnPropertyChanged([CallerMemberName] string prop = "") {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prop));
    }
}