using SmartSaleApp.Helpers;
using SmartSaleApp.Interfaces.ApiClients;

namespace SmartSaleApp.ViewModels;

public sealed class StartupViewModel : ViewModelBase {
    public bool IsVisible {
        get => _isVisible;
        set {
            if (_isVisible != value) {
                _isVisible = value;
                OnPropertyChanged();
            }
        }
    }

    private readonly ISecurityApiClient _securityApiClient;
    private bool _isVisible;

    public StartupViewModel(ISecurityApiClient securityApiClient) {
        _securityApiClient = securityApiClient;
    }

    public async Task LoadAsync() {
        await ExecuteAsync(async () => {
            await _securityApiClient.ValidateTokenAsync();
            PageHelper.Current = new AppShell();
        });

        if (!IsVisible) {
            IsVisible = true;
        }
    }
}