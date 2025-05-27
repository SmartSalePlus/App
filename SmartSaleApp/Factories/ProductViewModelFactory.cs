using SmartSaleApp.Interfaces.ApiClients;
using SmartSaleApp.Interfaces.Factory;
using SmartSaleApp.ViewModels;

namespace SmartSaleApp.Factories;

public sealed class ProductViewModelFactory : IProductViewModelFactory {
    private readonly IProductModalViewModelFactory _productModalViewModelFactory;
    private readonly IProductApiClient _productApiClient;

    public ProductViewModelFactory(IProductModalViewModelFactory productModalViewModelFactory, IProductApiClient productApiClient) {
        _productModalViewModelFactory = productModalViewModelFactory;
        _productApiClient = productApiClient;
    }

    public ProductViewModel Create(INavigation navigation) {
        return new(_productModalViewModelFactory, _productApiClient, navigation);
    }
}