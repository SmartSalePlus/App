using SmartSaleApp.Interfaces.Factory;
using SmartSaleApp.Models;
using SmartSaleApp.Pages;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Input;

namespace SmartSaleApp.ViewModels;

public sealed class HomeViewModel : INotifyPropertyChanged {
    public event PropertyChangedEventHandler? PropertyChanged;
    public ICommand AddCommand { get; }
    public ObservableCollection<InvoiceDetail> InvoiceDetails { get; } = [];

    public DateOnly Date {
        get => _date;
        set {
            if (_date != value) {
                _date = value;
                OnPropertyChanged();
            }
        }
    }

    public string BuyerName {
        get => _buyerName;
        set {
            if (_buyerName != value) {
                _buyerName = value;
                OnPropertyChanged();
            }
        }
    }

    private readonly IHomeModalViewModelFactory _homeModalViewModelFactory;
    private readonly INavigation _navigation;
    private string _buyerName;
    private DateOnly _date;
    private int _number;

    public HomeViewModel(IHomeModalViewModelFactory homeModalViewModelFactory, INavigation navigation) {
        _homeModalViewModelFactory = homeModalViewModelFactory;
        _navigation = navigation;
        _buyerName = string.Empty;
        _number = 1;
        AddCommand = new Command(async () => await OpenModalPageAsync());
    }

    private async Task OpenModalPageAsync() {
        var homeModalViewModel = _homeModalViewModelFactory.Create(_navigation, OnAdded, _number);
        var homeModalPage = new HomeModalPage(homeModalViewModel);
        await _navigation.PushModalAsync(homeModalPage);
    }

    private void OnAdded(InvoiceDetail invoiceDetail) {
        InvoiceDetails.Add(invoiceDetail);
        OnPropertyChanged(nameof(InvoiceDetails));
        _number++;
    }

    private void OnPropertyChanged([CallerMemberName] string prop = "") {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prop));
    }
}