using SmartSaleApp.ViewModels;

namespace SmartSaleApp.Pages;

public partial class ProductModalPage : ContentPage {
    public ProductModalPage(ProductModalViewModel productModalViewModel) {
        InitializeComponent();
        BindingContext = productModalViewModel;
    }

    private void Close(object sender, EventArgs e) {
        Navigation.PopModalAsync();
    }
}