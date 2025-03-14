using SmartSaleApp.Interfaces.Factory;

namespace SmartSaleApp.Pages;

public partial class HomePage : ContentPage
{
	public HomePage(IHomeViewModelFactory homeViewModelFactory)
	{
		InitializeComponent();
		BindingContext = homeViewModelFactory.Create(Navigation);
	}
}