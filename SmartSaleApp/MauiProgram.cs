using Microsoft.Extensions.Logging;
using SmartSaleApp.Extensions.DI;
using SmartSaleApp.Factories;
using SmartSaleApp.Interfaces.Factory;
using SmartSaleApp.Pages;
using SmartSaleApp.ViewModels;

namespace SmartSaleApp;

public static class MauiProgram {
    public static MauiApp CreateMauiApp() {
        var builder = MauiApp.CreateBuilder();
        builder
            .UseMauiApp<App>()
            .ConfigureFonts(fonts => {
                fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
            });
        builder.Services.AddApiClients();

        builder.Services.AddScoped<HomePage>();
        builder.Services.AddScoped<HomeModalPage>();
        builder.Services.AddScoped<HistoryPage>();

        builder.Services.AddScoped<HomeViewModel>();
        builder.Services.AddScoped<HomeModalViewModel>();
        builder.Services.AddScoped<HistoryViewModel>();

        builder.Services.AddScoped<IHomeViewModelFactory, HomeViewModelFactory>();
        builder.Services.AddScoped<IHomeModalViewModelFactory, HomeModalViewModelFactory>();

#if DEBUG
        builder.Logging.AddDebug();
#endif

        return builder.Build();
    }
}
