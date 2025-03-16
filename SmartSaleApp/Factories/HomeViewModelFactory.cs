using SmartSaleApp.Interfaces.ApiClients;
using SmartSaleApp.Interfaces.Factory;
using SmartSaleApp.ViewModels;
namespace SmartSaleApp.Factories;

public sealed class HomeViewModelFactory : IHomeViewModelFactory {
    private readonly IHomeModalViewModelFactory _homeModalViewModelFactory;
    private readonly IBuyerApiClient _buyerApiClient;
    private readonly IInvoiceApiClient _invoiceApiClient;

    public HomeViewModelFactory(
        IHomeModalViewModelFactory homeModalViewModelFactory, 
        IBuyerApiClient buyerApiClient,
        IInvoiceApiClient invoiceApiClient
    ) {
        _homeModalViewModelFactory = homeModalViewModelFactory;
        _buyerApiClient = buyerApiClient;
        _invoiceApiClient = invoiceApiClient;
    }

    public HomeViewModel Create(INavigation navigation) {
        return new HomeViewModel(_homeModalViewModelFactory, _buyerApiClient, _invoiceApiClient, navigation);
    }
}