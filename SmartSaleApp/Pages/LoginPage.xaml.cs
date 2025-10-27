using SmartSaleApp.Interfaces.ApiClients;
using SmartSaleApp.ViewModels;

namespace SmartSaleApp.Pages;

public partial class LoginPage : ContentPage {
    public LoginPage(ISecurityApiClient securityApiClient) {
        InitializeComponent();
        BindingContext = new LoginViewModel(securityApiClient);
    }
}