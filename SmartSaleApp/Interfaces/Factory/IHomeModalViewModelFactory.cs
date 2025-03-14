using SmartSaleApp.Models;
using SmartSaleApp.ViewModels;

namespace SmartSaleApp.Interfaces.Factory;

public interface IHomeModalViewModelFactory {
    HomeModalViewModel Create(INavigation navigation, Action<InvoiceDetail> invoiceDetailAddedHandler, int number);
}