using SmartSaleApp.Models.View;
using SmartSaleApp.ViewModels;

namespace SmartSaleApp.Interfaces.Factory;

public interface IHomeModalViewModelFactory {
    HomeModalViewModel Create(INavigation navigation, InvoiceDetailDto invoiceDetailDto, bool isAdd);
}