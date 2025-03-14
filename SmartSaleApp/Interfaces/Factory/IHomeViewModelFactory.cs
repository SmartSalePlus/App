using SmartSaleApp.ViewModels;

namespace SmartSaleApp.Interfaces.Factory;

public interface IHomeViewModelFactory {
    HomeViewModel Create(INavigation navigation);
}