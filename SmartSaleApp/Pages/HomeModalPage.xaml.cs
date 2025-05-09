using SmartSaleApp.ViewModels;

namespace SmartSaleApp.Pages;

public partial class HomeModalPage : ContentPage
{
	public HomeModalPage(HomeModalViewModel homeModalViewModel)
	{
		InitializeComponent();
		BindingContext = homeModalViewModel;
	}

    private void Close(object sender, EventArgs e) {
        Navigation.PopModalAsync();
    }
}