using SmartSaleApp.Dto;
using SmartSaleApp.Extensions;
using SmartSaleApp.Interfaces.ApiClients;
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
    public ICommand SaveCommand { get; }
    public ICommand SetIsPaidCommand { get; }
    public ObservableCollection<Buyer> Buyers { get; } = [];
    public ObservableCollection<InvoiceDetailDto> InvoiceDetails { get; } = [];

    public DateOnly Date {
        get => _invoice.Date;
        set {
            if (_invoice.Date != value) {
                _invoice.Date = value;
                OnPropertyChanged();
            }
        }
    }

    public Buyer? Buyer {
        get => _invoice.Buyer;
        set {
            if (_invoice.Buyer != value) {
                _invoice.Buyer = value;
                OnPropertyChanged();
            }
        }
    }

    public double? Total {
        get => _invoice.Total;
        set {
            if (_invoice.Total != value) {
                _invoice.Total = value;
                OnPropertyChanged();
                TotalWithDiscount = GetTotalWithDiscount();
            }
        }
    }

    public double? Discount {
        get => _invoice.Discount;
        set {
            if (_invoice.Discount != value) {
                _invoice.Discount = value;
                OnPropertyChanged();
                TotalWithDiscount = GetTotalWithDiscount();
            }
        }
    }

    public double? TotalWithDiscount {
        get => _invoice.TotalWithDiscount;
        set {
            if (_invoice.TotalWithDiscount != value) {
                _invoice.TotalWithDiscount = value;
                OnPropertyChanged();
            }
        }
    }

    public bool IsPaid {
        get => _invoice.IsPaid;
        set {
            if (_invoice.IsPaid != value) {
                _invoice.IsPaid = value;
                OnPropertyChanged();
            }
        }
    }

    private readonly IHomeModalViewModelFactory _homeModalViewModelFactory;
    private readonly IBuyerApiClient _buyerApiClient;
    private readonly IInvoiceApiClient _invoiceApiClient;
    private readonly INavigation _navigation;
    private readonly InvoiceDto _invoice;
    private int _number;

    public HomeViewModel(
        IHomeModalViewModelFactory homeModalViewModelFactory,
        IBuyerApiClient buyerApiClient,
        IInvoiceApiClient invoiceApiClient,
        INavigation navigation
    ) {
        _homeModalViewModelFactory = homeModalViewModelFactory;
        _buyerApiClient = buyerApiClient;
        _invoiceApiClient = invoiceApiClient;
        _navigation = navigation;
        _invoice = new();
        _ = GetBuyersAsync();
        _number = 1;
        AddCommand = new Command(async () => await OpenModalPageAsync());
        SaveCommand = new Command(async () => await SaveAsync(), IsValid);
        SetIsPaidCommand = new Command(() => IsPaid = !IsPaid);
    }

    private async Task GetBuyersAsync() {
        var buyers = await _buyerApiClient.GetAsync();
        foreach (var buyer in buyers) {
            Buyers.Add(buyer);
        }
    }

    private async Task OpenModalPageAsync() {
        var homeModalViewModel = _homeModalViewModelFactory.Create(_navigation, OnAdded, _number);
        var homeModalPage = new HomeModalPage(homeModalViewModel);
        await _navigation.PushModalAsync(homeModalPage);
    }

    private void OnAdded(InvoiceDetailDto invoiceDetail) {
        InvoiceDetails.Add(invoiceDetail);
        Total = InvoiceDetails.Sum(x => x.Total);
        _number++;
    }

    private async Task SaveAsync() {
        _invoice.InvoiceDetails = InvoiceDetails.ToModel();
        await _invoiceApiClient.AddAsync(_invoice.ToModel());
    }

    private double? GetTotalWithDiscount() {
        return Math.Round((Total ?? 0) - (Discount ?? 0));
    }

    private bool IsValid() {
        return TotalWithDiscount > 0;
    }

    private void OnPropertyChanged([CallerMemberName] string prop = "") {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prop));
    }
}