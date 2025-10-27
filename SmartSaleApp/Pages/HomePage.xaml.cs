using SmartSaleApp.ViewModels;

namespace SmartSaleApp.Pages;

public partial class HomePage : ContentPage {
    public HomePage(HomeViewModel homeViewModel) {
        InitializeComponent();
        BindingContext = homeViewModel;
    }
}