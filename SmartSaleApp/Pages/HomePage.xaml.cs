using SmartSaleApp.ViewModelsl;

namespace SmartSaleApp.Pages;

public partial class HomePage : ContentPage
{
	public HomePage(HomeViewModel homeViewModel)
	{
		InitializeComponent();
		homeViewModel.Navigation = Navigation;
		BindingContext = homeViewModel;
	}
}