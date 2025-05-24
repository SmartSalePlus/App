using SmartSaleApp.ViewModels;

namespace SmartSaleApp.Pages;

public partial class HistoryPage : ContentPage {
    public HistoryPage(HistoryViewModel historyViewModel) {
        InitializeComponent();
        BindingContext = historyViewModel;
    }
}