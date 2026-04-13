using SmartSaleApp.Helpers;
using SmartSaleApp.Interfaces.ApiClients;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Input;

namespace SmartSaleApp.ViewModels;

public sealed class LoginViewModel : INotifyPropertyChanged {
    public event PropertyChangedEventHandler? PropertyChanged;
    public ICommand LoginCommand { get; }

    public string Login {
        get => _login;
        set {
            if (_login != value) {
                _login = value;
                OnPropertyChanged();
                ((Command)LoginCommand).ChangeCanExecute();
            }
        }
    }

    public string Password {
        get => _password;
        set {
            if (_password != value) {
                _password = value;
                OnPropertyChanged();
                ((Command)LoginCommand).ChangeCanExecute();
            }
        }
    }

    public string? Error {
        get => _error;
        set {
            if (_error != value) {
                _error = value;
                OnPropertyChanged();
            }
        }
    }

    private readonly ISecurityApiClient _authApiClient;
    private string _login = "Ramil";
    private string _password = "Ramil1998";
    private string? _error;

    public LoginViewModel(ISecurityApiClient authApiClient) {
        _authApiClient = authApiClient;
        LoginCommand = new Command(async () => await SignIn(), IsValid);
    }

    private async Task SignIn() {
        var token = await _authApiClient.LoginAsync(new(_login, _password));
        JwtHelper.SetToken(token);
        PageHelper.Current = new AppShell();
    }

    private bool IsValid() {
        return !string.IsNullOrEmpty(_login) && !string.IsNullOrEmpty(_password);
    }

    private void OnPropertyChanged([CallerMemberName] string prop = "") {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prop));
    }
}