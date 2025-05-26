using SmartSaleApp.Interfaces.ApiClients;
using SmartSaleApp.Interfaces.Factory;
using SmartSaleApp.Models.View;
using SmartSaleApp.ViewModels;

namespace SmartSaleApp.Factories;

public sealed class HomeModalViewModelFactory : IHomeModalViewModelFactory {
    private readonly IProductApiClient _productApiClient;

    public HomeModalViewModelFactory(IProductApiClient productApiClient) {
        _productApiClient = productApiClient;
    }

    public HomeModalViewModel Create(INavigation navigation, InvoiceDetailDto invoiceDetailDto, bool isAdd) {
        return new(_productApiClient, navigation, invoiceDetailDto, isAdd);
    }
}