using CommunityToolkit.Maui.Storage;
using SmartSaleApp.Helpers;
using SmartSaleApp.Interfaces.ApiClients;
using SmartSaleApp.Models.Data;
using System.Collections.ObjectModel;
using System.Globalization;
using System.Windows.Input;

namespace SmartSaleApp.ViewModels;

public sealed class BuyerViewModel : ViewModelBase {
    public ICommand AddCommand { get; }
    public ICommand GetFileCommand { get; }
    public ObservableCollection<Buyer> Buyers { get; private set; } = [];

    public Buyer? Buyer {
        get => null;
        set {
            if (value != null) {
                _ = EditAsync(value);
                OnPropertyChanged();
            }
        }
    }

    private readonly IBuyerApiClient _buyerApiClient;
    private readonly IFileSaver _fileSaver;

    public BuyerViewModel(IBuyerApiClient buyerApiClient, IFileSaver fileSaver) {
        _buyerApiClient = buyerApiClient;
        _fileSaver = fileSaver;
        AddCommand = new Command(async () => await AddAsync());
        GetFileCommand = new Command<Buyer>(async (x) => await GetFileAsync(x));
    }

    public async Task LoadAsync() {
        await ExecuteAsync(GetAsync);
    }

    private async Task GetAsync() {
        var buyers = await _buyerApiClient.GetAsync();
        Buyers = new(buyers);
        OnPropertyChanged(nameof(Buyers));
    }

    private async Task AddAsync() {
        var name = await PageHelper.Current.DisplayPromptAsync("Добавление клиента", "Введите имя:", "Готово", "Отмена");

        if (string.IsNullOrWhiteSpace(name)) {
            return;
        }

        await ExecuteAsync(async () => {
            await _buyerApiClient.AddAsync(new(0, name));
            await GetAsync();
        });
    }

    private async Task EditAsync(Buyer buyer) {
        var name = await PageHelper.Current.DisplayPromptAsync("Редактирование клиента", "Введите имя:", "Готово", "Отмена", initialValue: buyer.Name);

        if (string.IsNullOrWhiteSpace(name) || buyer.Name == name) {
            return;
        }

        await ExecuteAsync(async () => {
            await _buyerApiClient.UpdateAsync(new(buyer.Id, name));
            await GetAsync();
        });
    }

    private async Task GetFileAsync(Buyer buyer) {
        await ExecuteAsync(async () => {
            var file = await _buyerApiClient.GetFile(buyer.Id);
            var date = DateTime.Now.ToString("dd-MM-yyyy", CultureInfo.InvariantCulture);
            var name = $"{buyer.Name}_{date}.pdf";
            await _fileSaver.SaveAsync(name, file);
        });
    }
}