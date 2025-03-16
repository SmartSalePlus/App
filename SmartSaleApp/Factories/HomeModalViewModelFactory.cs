using SmartSaleApp.Dto;
using SmartSaleApp.Interfaces.ApiClients;
using SmartSaleApp.Interfaces.Factory;
using SmartSaleApp.ViewModels;

namespace SmartSaleApp.Factories;

public sealed class HomeModalViewModelFactory : IHomeModalViewModelFactory {
    private readonly IProductApiClient _productApiClient;

    public HomeModalViewModelFactory(IProductApiClient productApiClient) {
        _productApiClient = productApiClient;
    }

    public HomeModalViewModel Create(INavigation navigation, Action<InvoiceDetailDto> invoiceDetailAddedHandler, int number) {
        return new(_productApiClient, navigation, invoiceDetailAddedHandler, number);
    }
}