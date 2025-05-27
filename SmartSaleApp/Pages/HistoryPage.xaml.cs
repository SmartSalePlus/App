using SmartSaleApp.Interfaces.Factory;
using SmartSaleApp.ViewModels;

namespace SmartSaleApp.Pages;

public partial class HistoryPage : ContentPage {
    private readonly IHistoryViewModelFactory _historyViewModelFactory;

    public HistoryPage(IHistoryViewModelFactory historyViewModelFactory) {
        InitializeComponent();
        _historyViewModelFactory = historyViewModelFactory;
        var historyViewModel = _historyViewModelFactory.Create();
        BindingContext = historyViewModel;
    }

    private async void Reset(object sender, EventArgs e) {
        bool isReset = await DisplayAlert("Подтвердить действие", "Вы хотите все сбросить?", "Да", "Нет");

        if (isReset) {
            OnReset();
        }
    }

    private void OnReset() {
        BindingContext = _historyViewModelFactory.Create();
    }
}