using SmartSaleApp.Interfaces.ApiClients;
using SmartSaleApp.Interfaces.Factory;
using SmartSaleApp.Models.View;
using SmartSaleApp.ViewModels;

namespace SmartSaleApp.Factories;

public sealed class ProductModalViewModelFactory : IProductModalViewModelFactory {
    private readonly IProductApiClient _productApiClient;

    public ProductModalViewModelFactory(IProductApiClient productApiClient) {
        _productApiClient = productApiClient;
    }

    public ProductModalViewModel Create(INavigation navigation, ProductDto productDto, bool isAdd) {
        return new(_productApiClient, navigation, productDto, isAdd);
    }
}