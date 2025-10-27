using SmartSaleApp.Pages;

namespace SmartSaleApp;

public partial class App : Application {
    public App(StartupPage startupPage) {
        InitializeComponent();
        MainPage = new NavigationPage(startupPage);
    }
}