using SmartSaleApp.Models;
using SmartSaleApp.Pages;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows.Input;

namespace SmartSaleApp.ViewModelsl;

public sealed class HomeViewModel : INotifyPropertyChanged {
    public event PropertyChangedEventHandler? PropertyChanged;

    public ICommand AddCommand => new Command(Add);

    public ObservableCollection<InvoiceDetail> InvoiceDetails { get; set; } = [];

    private string _buyerName = string.Empty;

    private DateTime _date;

    private int _number;

    public INavigation Navigation { get; set; }

    private HomeModalPage _homeModalPage;

    public HomeViewModel(HomeModalPage homeModalPage) {
        _homeModalPage = homeModalPage;
        _homeModalPage.InvoiceDetails = InvoiceDetails;
    }

    private async void Add() {
        await Navigation.PushModalAsync(_homeModalPage);
    }
}