using SmartSaleApp.Interfaces.Factory;

namespace SmartSaleApp.Pages;

public partial class ProductPage : ContentPage {
    private readonly IProductViewModelFactory _productViewModelFactory;

    public ProductPage(IProductViewModelFactory productViewModelFactory) { 
        InitializeComponent();
        _productViewModelFactory = productViewModelFactory;
        var productViewModel = _productViewModelFactory.Create(Navigation);
        BindingContext = productViewModel;
    }
}