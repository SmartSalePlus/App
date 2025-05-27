using SmartSaleApp.Interfaces.ApiClients;
using SmartSaleApp.Interfaces.Factory;
using SmartSaleApp.ViewModels;

namespace SmartSaleApp.Factories;
public sealed class HistoryViewModelFactory : IHistoryViewModelFactory {
    private readonly IInvoiceApiClient _invoiceApiClient;
    private readonly IBuyerApiClient _buyerApiClient;

    public HistoryViewModelFactory(IInvoiceApiClient invoiceApiClient, IBuyerApiClient buyerApiClient) {
        _invoiceApiClient = invoiceApiClient;
        _buyerApiClient = buyerApiClient;
    }

    public HistoryViewModel Create() {
        return new(_invoiceApiClient, _buyerApiClient);
    }
}