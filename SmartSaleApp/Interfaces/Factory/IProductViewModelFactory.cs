using SmartSaleApp.ViewModels;

namespace SmartSaleApp.Interfaces.Factory;

public interface IProductViewModelFactory {
    ProductViewModel Create(INavigation navigation);
}