using SmartSaleApp.Interfaces.ApiClients;
using SmartSaleApp.Interfaces.Factory;
using SmartSaleApp.ViewModels;

namespace SmartSaleApp.Factories;

public sealed class AddHomeViewModelFactory : IAddHomeViewModelFactory {
    private readonly IProductApiClient _productApiClient;

    public AddHomeViewModelFactory(IProductApiClient productApiClient) {
        _productApiClient = productApiClient;
    }

    public AddHomeViewModel Create(int number) {
        return new(_productApiClient, number);
    }
}