using SmartSaleApp.Extensions.Mapping;
using SmartSaleApp.Helpers;
using SmartSaleApp.Interfaces.ApiClients;
using SmartSaleApp.Models.Data;
using SmartSaleApp.Models.View;
using SmartSaleApp.Pages;
using System.Collections.ObjectModel;
using System.Windows.Input;

namespace SmartSaleApp.ViewModels;

public sealed class ProductViewModel : ViewModelBase {
    public ICommand AddCommand { get; }
    public ObservableCollection<ProductDto> ProductDtos { get; private set; } = [];

    public ProductDto? ProductDto {
        get => null;
        set {
            if (value != null) {
                _ = OpenModalPageAsync(value.Clone(), UpdateAsync);
                OnPropertyChanged();
            }
        }
    }

    private readonly IProductApiClient _productApiClient;

    public ProductViewModel(IProductApiClient productApiClient) {
        _productApiClient = productApiClient;
        AddCommand = new Command(async () => await OpenModalPageAsync(new(), AddAsync));
    }

    public async Task LoadAsync() {
        await ExecuteAsync(GetAsync);
    }

    private async Task GetAsync() {
        var products = await _productApiClient.GetAsync();
        ProductDtos = new(products.ToDto());
        OnPropertyChanged(nameof(ProductDtos));
    }

    private async Task OpenModalPageAsync(ProductDto productDto, Func<Product, Task> saveHandler) {
        ProductModalViewModel productModalViewModel = new(productDto, saveHandler);
        await PageHelper.Current.Navigation.PushModalAsync(new ProductModalPage(productModalViewModel));
    }

    private async Task AddAsync(Product product) {
        await ExecuteAsync(async () => {
            await _productApiClient.AddAsync(product);
            await GetAsync();
        });
    }

    private async Task UpdateAsync(Product product) {
        await ExecuteAsync(async () => {
            await _productApiClient.UpdateAsync(product);
            await GetAsync();
        });
    }
}