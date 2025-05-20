using SmartSaleApp.Interfaces.Factory;

namespace SmartSaleApp.Pages;

public partial class HomePage : ContentPage {
    private readonly IHomeViewModelFactory _homeViewModelFactory;

    public HomePage(IHomeViewModelFactory homeViewModelFactory) {
        InitializeComponent();
        _homeViewModelFactory = homeViewModelFactory;
        var homeViewModel = _homeViewModelFactory.Create(Navigation);
        homeViewModel.Saved += OnReset;
        BindingContext = homeViewModel;
    }

    private async void Reset(object sender, EventArgs e) {
        bool isReset = await DisplayAlert("Подтвердить действие", "Вы хотите все сбросить?", "Да", "Нет");

        if (isReset) {
            OnReset();
        }
    }

    private void OnReset() {
        BindingContext = _homeViewModelFactory.Create(Navigation);
    }
}