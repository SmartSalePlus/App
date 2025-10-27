using SmartSaleApp.Interfaces.ApiClients;
using SmartSaleApp.ViewModels;

namespace SmartSaleApp.Pages;

public partial class StartupPage : ContentPage {
    private readonly StartupViewModel _startupViewModel;

    public StartupPage(ISecurityApiClient securityApiClient) {
        InitializeComponent();
        _startupViewModel = new(securityApiClient);
        BindingContext = _startupViewModel;
    }

    protected override async void OnAppearing() {
        base.OnAppearing();
        await _startupViewModel.LoadAsync();
    }

    private async void SignIn(object sender, EventArgs e) {
        await _startupViewModel.LoadAsync();
    }
}