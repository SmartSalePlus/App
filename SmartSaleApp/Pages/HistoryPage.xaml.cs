using SmartSaleApp.Interfaces.ApiClients;
using SmartSaleApp.ViewModels;

namespace SmartSaleApp.Pages;

public partial class HistoryPage : ContentPage {
    private readonly IInvoiceApiClient _invoiceApiClient;
    private readonly IBuyerApiClient _buyerApiClient;
    private readonly HistoryViewModel _historyViewModel;

    public HistoryPage(IInvoiceApiClient invoiceApiClient, IBuyerApiClient buyerApiClient) {
        InitializeComponent();
        _invoiceApiClient = invoiceApiClient;
        _buyerApiClient = buyerApiClient;
        _historyViewModel = new HistoryViewModel(_invoiceApiClient, _buyerApiClient);
        BindingContext = _historyViewModel;
    }

    protected override async void OnAppearing() {
        base.OnAppearing();
        await _historyViewModel.LoadAsync();
    }

    private async void Reset(object sender, EventArgs e) {
        bool isReset = await DisplayAlert("Подтвердить действие", "Вы хотите все сбросить?", "Да", "Нет");

        if (isReset) {
            OnReset();
        }
    }

    private void OnReset() {
        BindingContext = new HistoryViewModel(_invoiceApiClient, _buyerApiClient);
    }
}