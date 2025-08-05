using SmartSaleApp.Extensions.Mapping;
using SmartSaleApp.Interfaces.ApiClients;
using SmartSaleApp.Interfaces.Factory;
using SmartSaleApp.Models.Data;
using SmartSaleApp.Models.View;
using SmartSaleApp.Pages;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Input;

namespace SmartSaleApp.ViewModels;

public sealed class HomeViewModel : INotifyPropertyChanged {
    public event PropertyChangedEventHandler? PropertyChanged;
    public event Action? Saved;
    public ICommand AddCommand { get; }
    public ICommand SaveCommand { get; }
    public ICommand SetIsPaidCommand { get; }
    public ObservableCollection<Buyer> Buyers { get; private set; } = [];
    public ObservableCollection<InvoiceDetailDto> InvoiceDetailDtos { get; } = [];
    public DateTime Date { get; set; }

    public Buyer? Buyer {
        get => _invoiceDto.Buyer;
        set {
            if (_invoiceDto.Buyer != value) {
                _invoiceDto.Buyer = value;
                OnPropertyChanged();
                ((Command)SaveCommand).ChangeCanExecute();
            }
        }
    }

    public double? Total {
        get => _invoiceDto.Total;
        set {
            if (_invoiceDto.Total != value) {
                _invoiceDto.Total = value ?? 0;
                OnPropertyChanged();
                TotalWithDiscount = GetTotalWithDiscount();
            }
        }
    }

    public double? Discount {
        get => _invoiceDto.Discount;
        set {
            if (_invoiceDto.Discount != value) {
                _invoiceDto.Discount = value;
                OnPropertyChanged();
                TotalWithDiscount = GetTotalWithDiscount();
            }
        }
    }

    public double? TotalWithDiscount {
        get => _invoiceDto.TotalWithDiscount;
        set {
            if (_invoiceDto.TotalWithDiscount != value) {
                _invoiceDto.TotalWithDiscount = value ?? 0;
                OnPropertyChanged();
                ((Command)SaveCommand).ChangeCanExecute();
            }
        }
    }

    public bool IsPaid {
        get => _invoiceDto.IsPaid;
        set {
            if (_invoiceDto.IsPaid != value) {
                _invoiceDto.IsPaid = value;
                OnPropertyChanged();
            }
        }
    }

    public InvoiceDetailDto? InvoiceDetailDto {
        get => null;
        set {
            if (value != null) {
                _ = EditAsync(value);
                OnPropertyChanged();
            }
        }
    }

    private readonly IHomeModalViewModelFactory _homeModalViewModelFactory;
    private readonly IBuyerApiClient _buyerApiClient;
    private readonly IInvoiceApiClient _invoiceApiClient;
    private readonly INavigation _navigation;
    private readonly InvoiceDto _invoiceDto;
    private int _indexInvoiceDetailDto;

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
        Date = DateTime.Now;
        _invoiceDto = new();
        _ = GetBuyersAsync();
        AddCommand = new Command(async () => await AddAsync());
        SaveCommand = new Command(async () => await SaveAsync(), IsValid);
        SetIsPaidCommand = new Command(() => IsPaid = !IsPaid);
    }

    private async Task GetBuyersAsync() {
        var buyers = await _buyerApiClient.GetAsync();
        Buyers = new(buyers);
        OnPropertyChanged(nameof(Buyers));
    }

    private async Task AddAsync() {
        await OpenModalPageAsync(new(), true);
    }

    private async Task EditAsync(InvoiceDetailDto invoiceDetailDto) {
        _indexInvoiceDetailDto = InvoiceDetailDtos.IndexOf(invoiceDetailDto);
        var cloneInvoiceDetailDto = invoiceDetailDto.Clone();
        await OpenModalPageAsync(cloneInvoiceDetailDto, false);
    }

    private async Task OpenModalPageAsync(InvoiceDetailDto invoiceDetailDto, bool isAdd) {
        var homeModalViewModel = _homeModalViewModelFactory.Create(_navigation, invoiceDetailDto, isAdd);
        homeModalViewModel.Saved += OnAdded;
        homeModalViewModel.Removed += OnRemoved;
        var homeModalPage = new HomeModalPage(homeModalViewModel);
        await _navigation.PushModalAsync(homeModalPage);
    }

    private void OnAdded(InvoiceDetailDto invoiceDetailDto, bool isAdd) {
        if (isAdd) {
            InvoiceDetailDtos.Add(invoiceDetailDto);
        }
        else {
            InvoiceDetailDtos[_indexInvoiceDetailDto] = invoiceDetailDto;
        }
        Total = InvoiceDetailDtos.Sum(x => x.Total);
    }

    private void OnRemoved() {
        InvoiceDetailDtos.RemoveAt(_indexInvoiceDetailDto);
    }

    private async Task SaveAsync() {
        _invoiceDto.InvoiceDetailDtos = InvoiceDetailDtos;
        _invoiceDto.Date = DateOnly.FromDateTime(Date);
        await _invoiceApiClient.AddAsync(_invoiceDto.ToModel());
        Saved?.Invoke();
    }

    private double? GetTotalWithDiscount() {
        return Math.Round((Total ?? 0) - (Discount ?? 0));
    }

    private bool IsValid() {
        return Buyer != null && TotalWithDiscount > 0;
    }

    private void OnPropertyChanged([CallerMemberName] string prop = "") {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prop));
    }
}