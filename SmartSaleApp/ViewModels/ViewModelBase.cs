using Refit;
using SmartSaleApp.Helpers;
using System.ComponentModel;
using System.Net;
using System.Runtime.CompilerServices;

namespace SmartSaleApp.ViewModels;

public abstract class ViewModelBase : INotifyPropertyChanged {
    public event PropertyChangedEventHandler? PropertyChanged;

    public bool IsBusy {
        get => _isBusy;
        set {
            if (_isBusy != value) {
                _isBusy = value;
                OnPropertyChanged();
            }
        }
    }

    private bool _isBusy;

    protected async Task ExecuteAsync(Func<Task> execute) {
        ArgumentNullException.ThrowIfNull(execute);

        try {
            IsBusy = true;
            await execute();
        }
        catch (ApiException ex) when (ex.StatusCode == HttpStatusCode.Unauthorized) {
            var errorMessage = ex.Content;
            if (string.IsNullOrWhiteSpace(errorMessage)) {
                errorMessage = "Ваша сессия истекла. Пожалуйста, войдите снова.";
            }
            await PageHelper.Current.DisplayAlert("Ошибка авторизации", $"{errorMessage}{Environment.NewLine}{ex.Message}", "OK");
        }
        catch (Exception ex) {
            await PageHelper.Current.DisplayAlert("Ошибка", ex.Message, "OK");
        }
        finally {
            IsBusy = false;
        }
    }

    protected void OnPropertyChanged([CallerMemberName] string prop = "") {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prop));
    }
}