using CommunityToolkit.Maui.Storage;
using SmartSaleApp.Interfaces.ApiClients;
using SmartSaleApp.ViewModels;

namespace SmartSaleApp.Pages;

public partial class BuyerPage : ContentPage {
    private readonly BuyerViewModel _buyerViewModel;

    public BuyerPage(IBuyerApiClient buyerApiClient, IFileSaver fileSaver) {
        InitializeComponent();
        _buyerViewModel = new(buyerApiClient, fileSaver);
        BindingContext = _buyerViewModel;
    }

    protected override async void OnAppearing() {
        base.OnAppearing();
        await _buyerViewModel.LoadAsync();
    }
}