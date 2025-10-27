using SmartSaleApp.Pages;

namespace SmartSaleApp.Helpers;

public static class PageHelper {
    public static Page Current {
        get {
            ArgumentNullException.ThrowIfNull(Application.Current?.MainPage);
            return Application.Current.MainPage;
        }
        set {
            ArgumentNullException.ThrowIfNull(value);
            ArgumentNullException.ThrowIfNull(Application.Current?.MainPage);
            Application.Current.MainPage = value;
        }
    }

    public static bool IsLoginPage =>
        Current is LoginPage or NavigationPage { CurrentPage: LoginPage };
}