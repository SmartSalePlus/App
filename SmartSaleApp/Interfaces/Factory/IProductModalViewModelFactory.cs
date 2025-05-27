using SmartSaleApp.Models.View;
using SmartSaleApp.ViewModels;

namespace SmartSaleApp.Interfaces.Factory;

public interface IProductModalViewModelFactory {
    ProductModalViewModel Create(INavigation navigation, ProductDto productDto, bool isAdd);
}