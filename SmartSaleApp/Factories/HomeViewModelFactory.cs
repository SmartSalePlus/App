using SmartSaleApp.Interfaces.Factory;
using SmartSaleApp.ViewModels;
namespace SmartSaleApp.Factories;

public sealed class HomeViewModelFactory : IHomeViewModelFactory {
    private readonly IHomeModalViewModelFactory _homeModalViewModelFactory;

    public HomeViewModelFactory(IHomeModalViewModelFactory homeModalViewModelFactory) {
        _homeModalViewModelFactory = homeModalViewModelFactory;
    }

    public HomeViewModel Create(INavigation navigation) {
        return new HomeViewModel(_homeModalViewModelFactory, navigation);
    }
}