using SmartSaleApp.Interfaces.ApiClients;
using SmartSaleApp.ViewModels;

namespace SmartSaleApp.Pages;

public partial class ProductPage : ContentPage {
    private readonly ProductViewModel _productViewModel;

    public ProductPage(IProductApiClient productApiClient) {
        InitializeComponent();
        _productViewModel = new(productApiClient);
        BindingContext = _productViewModel;
    }

    protected override async void OnAppearing() {
        base.OnAppearing();
        await _productViewModel.LoadAsync();
    }
}