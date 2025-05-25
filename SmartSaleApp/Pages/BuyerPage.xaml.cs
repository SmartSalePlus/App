using SmartSaleApp.ViewModels;

namespace SmartSaleApp.Pages;

public partial class BuyerPage : ContentPage {
    public BuyerPage(BuyerViewModel buyerViewModel) {
        InitializeComponent();
        BindingContext = buyerViewModel;
    }
}