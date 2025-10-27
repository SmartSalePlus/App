using SmartSaleApp.Interfaces.ApiClients;
using SmartSaleApp.ViewModels;

namespace SmartSaleApp.Pages;

public partial class BuyerPage : ContentPage {
    private readonly BuyerViewModel _buyerViewModel;

    public BuyerPage(IBuyerApiClient buyerApiClient) {
        InitializeComponent();
        _buyerViewModel = new(buyerApiClient);
        BindingContext = _buyerViewModel;
    }

    protected override async void OnAppearing() {
        base.OnAppearing();
        await _buyerViewModel.LoadAsync();
    }
}