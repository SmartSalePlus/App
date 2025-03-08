using Microsoft.Extensions.Logging;
using SmartSaleApp.Extensions;
using SmartSaleApp.Pages;
using SmartSaleApp.ViewModels;
using SmartSaleApp.ViewModelsl;

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

        builder.Services.AddScoped<HomeViewModel>();
        builder.Services.AddScoped<HomeModalViewModel>();

#if DEBUG
        builder.Logging.AddDebug();
#endif

        return builder.Build();
    }
}
