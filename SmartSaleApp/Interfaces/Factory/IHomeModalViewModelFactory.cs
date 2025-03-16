using SmartSaleApp.Dto;
using SmartSaleApp.ViewModels;

namespace SmartSaleApp.Interfaces.Factory;

public interface IHomeModalViewModelFactory {
    HomeModalViewModel Create(INavigation navigation, Action<InvoiceDetailDto> invoiceDetailAddedHandler, int number);
}