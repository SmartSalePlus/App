using Refit;
using SmartSaleApp.Handlers;
using SmartSaleApp.Interfaces.ApiClients;

namespace SmartSaleApp.Extensions.DI;

internal static class ApiClientsRegisterExtension {
    public static IServiceCollection AddApiClients(this IServiceCollection services) {
        services.AddApiClient<IBuyerApiClient>("buyer");
        services.AddApiClient<IInvoiceApiClient>("invoice");
        services.AddApiClient<IProductApiClient>("product");
        services.AddApiClient<IReceptionApiClient>("reception");
        services.AddApiClient<ISecurityApiClient>("security");

        return services;
    }

    private static IServiceCollection AddApiClient<T>(this IServiceCollection services, string controllerName) where T : class {
        services.AddRefitClient<T>()
            .ConfigureHttpClient(x => x.BaseAddress = new Uri($"{GetBaseAddress()}/{controllerName}"))
            .AddHttpMessageHandler<AuthorizationHandler>()
            .ConfigurePrimaryHttpMessageHandler(() => {
                var handler = new HttpClientHandler();
#if DEBUG
                handler.ServerCertificateCustomValidationCallback = (message, cert, chain, errors) => {
                    if (cert != null && cert.Issuer.Equals("CN=localhost"))
                        return true;
                    return errors == System.Net.Security.SslPolicyErrors.None;
                };
#endif
                return handler;
            });

        return services;
    }

    private static string GetBaseAddress() {
        if (OperatingSystem.IsAndroid()) {
            return "https://10.0.2.2:7270/api";
        }

        return "https://localhost:7270/api";
    }
}