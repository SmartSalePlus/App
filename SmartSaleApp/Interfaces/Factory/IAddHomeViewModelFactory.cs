using SmartSaleApp.ViewModels;

namespace SmartSaleApp.Interfaces.Factory;

public interface IAddHomeViewModelFactory {
    AddHomeViewModel Create(int number);
}