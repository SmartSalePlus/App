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
        //JwtHelper.InitializeDatabase();
        var token = JwtHelper.GetToken();
        request.Headers.Authorization = new("Bearer", token);

        var response = await base.SendAsync(request, cancellationToken);
        if (response.StatusCode == HttpStatusCode.Unauthorized) {
            MainThread.BeginInvokeOnMainThread(() => {
                if (!PageHelper.IsLoginPage) {
                    JwtHelper.SetToken("");
                    PageHelper.Current = new NavigationPage(_loginPage.Value);
                }
            });
        }
        return response;
    }
}