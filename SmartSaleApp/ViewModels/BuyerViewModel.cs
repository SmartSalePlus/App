using SmartSaleApp.Interfaces.ApiClients;
using SmartSaleApp.Models.Data;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Input;

namespace SmartSaleApp.ViewModels;

public sealed class BuyerViewModel : INotifyPropertyChanged {
    public event PropertyChangedEventHandler? PropertyChanged;
    public ICommand LoadCommand { get; }
    public ICommand AddCommand { get; }
    //public ICommand DeleteCommand { get; }
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

    public BuyerViewModel(IBuyerApiClient buyerApiClient) {
        _buyerApiClient = buyerApiClient;
        _ = GetAsync();
        LoadCommand = new Command(async () => await GetAsync());
        AddCommand = new Command(async () => await AddAsync());
        //DeleteCommand = new Command<Buyer>(async (buyer) => await DeleteAsync(buyer));
    }

    public async Task AddAsync() {
        var name = await DisplayPromptAsync();

        if (string.IsNullOrWhiteSpace(name)) {
            return;
        }

        var buyer = new Buyer(0, name);
        await _buyerApiClient.AddAsync(buyer);
        await GetAsync();
    }

    private async Task GetAsync() {
        var buyers = await _buyerApiClient.GetAsync();
        Buyers = new(buyers);
        OnPropertyChanged(nameof(Buyers));
    }

    private async Task EditAsync(Buyer buyer) {
        var name = await DisplayPromptAsync(buyer.Name);

        if (string.IsNullOrWhiteSpace(name)) {
            return;
        }

        if (buyer.Name == name) {
            return;
        }

        await _buyerApiClient.UpdateAsync(new(buyer.Id, name));
        await GetAsync();
    }

    private async Task<string> DisplayPromptAsync(string initialValue = "") {
        var page = Application.Current?.MainPage;

        if (page == null) {
            return string.Empty;
        }

        string title = string.IsNullOrWhiteSpace(initialValue) ? "Добавление клиента" : "Редактирование клиента";

        return await page.DisplayPromptAsync(title, "Введите имя:", "Готово", "Отмена", initialValue: initialValue);
    }

    //private async Task DeleteAsync(Buyer buyer) {
    //    await _buyerApiClient.DeleteAsync(buyer.Id);
    //    await GetAsync();
    //}

    private void OnPropertyChanged([CallerMemberName] string prop = "") {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prop));
    }
}