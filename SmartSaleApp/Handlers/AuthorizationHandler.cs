using SmartSaleApp.Helpers;
using SmartSaleApp.Pages;
using System.Net;

namespace SmartSaleApp.Handlers;

public sealed class AuthorizationHandler : DelegatingHandler {
    private readonly Lazy<LoginPage> _loginPage;
    public AuthorizationHandler(IServiceProvider serviceProvider) {
        _loginPage = new Lazy<LoginPage>(serviceProvider.GetRequiredService<LoginPage>);
    }

    protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken) {
        var token = await JwtHelper.GetTokenAsync();
        request.Headers.Authorization = new("Bearer", token);

        var response = await base.SendAsync(request, cancellationToken);
        if (response.StatusCode == HttpStatusCode.Unauthorized) {
            MainThread.BeginInvokeOnMainThread(async () => {
                if (!PageHelper.IsLoginPage) {
                    await JwtHelper.SetTokenAsync("");
                    PageHelper.Current = new NavigationPage(_loginPage.Value);
                }
            });
        }
        return response;
    }
}