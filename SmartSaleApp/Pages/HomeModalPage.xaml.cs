using SmartSaleApp.Models;
using SmartSaleApp.ViewModels;
using System.Collections.ObjectModel;

namespace SmartSaleApp.Pages;

public partial class HomeModalPage : ContentPage
{
	public ObservableCollection<InvoiceDetail> InvoiceDetails { get; set; }

	public HomeModalPage(HomeModalViewModel homeModalViewModel)
	{
		InitializeComponent();
		homeModalViewModel.Navigation = Navigation;
		homeModalViewModel.InvoiceDetails = InvoiceDetails;
		BindingContext = homeModalViewModel;
	}
}