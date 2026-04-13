using SmartSaleApp.Extensions.Mapping;
using SmartSaleApp.Helpers;
using SmartSaleApp.Interfaces.ApiClients;
using SmartSaleApp.Models.Data;
using SmartSaleApp.Models.View;
using SmartSaleApp.Pages;
using System.Collections.ObjectModel;
using System.Windows.Input;

namespace SmartSaleApp.ViewModels;

public sealed class HomeViewModel : ViewModelBase {
    public ICommand AddCommand { get; }
    public ICommand SaveCommand { get; }
    public ICommand ResetCommand { get; }
    public ICommand SetIsPaidCommand { get; }
    public ObservableCollection<Buyer> Buyers { get; private set; } = [];
    public ObservableCollection<InvoiceDetailDto> InvoiceDetailDtos { get; private set; } = [];
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

    public double Total {
        get => _invoiceDto.Total;
        set {
            if (_invoiceDto.Total != value) {
                _invoiceDto.Total = value;
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

    public double TotalWithDiscount {
        get => _invoiceDto.TotalWithDiscount;
        set {
            if (_invoiceDto.TotalWithDiscount != value) {
                _invoiceDto.TotalWithDiscount = value;
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
                _index = InvoiceDetailDtos.IndexOf(value);
                _ = OpenModalPageAsync(value.Clone(), Update, Delete);
                OnPropertyChanged();
            }
        }
    }

    private readonly IBuyerApiClient _buyerApiClient;
    private readonly IInvoiceApiClient _invoiceApiClient;
    private readonly IProductApiClient _productApiClient;
    private readonly InvoiceDto _invoiceDto;
    private int _index;
    private IEnumerable<ProductDto> _productDtos = [];

    public HomeViewModel(
        IBuyerApiClient buyerApiClient,
        IInvoiceApiClient invoiceApiClient,
        IProductApiClient productApiClient
    ) {
        _buyerApiClient = buyerApiClient;
        _invoiceApiClient = invoiceApiClient;
        _productApiClient = productApiClient;
        Date = DateTime.Now;
        _invoiceDto = new();
        _ = LoadAsync();
        AddCommand = new Command(async () => await OpenModalPageAsync(new(), Add));
        SaveCommand = new Command(async () => await SaveAsync(), IsValid);
        ResetCommand = new Command(async () => await ConfirmResetAsync());
        SetIsPaidCommand = new Command(() => IsPaid = !IsPaid);
    }

    public async Task LoadAsync() {
        await ExecuteAsync(async () => {
            var buyers = await _buyerApiClient.GetAsync();
            Buyers = new(buyers);
            OnPropertyChanged(nameof(Buyers));
            _productDtos = (await _productApiClient.GetAsync()).ToDto();
        });
    }

    private async Task OpenModalPageAsync(
        InvoiceDetailDto invoiceDetailDto,
        Action<InvoiceDetailDto> saveHandler,
        Action? deleteHandler = null
    ) {
        HomeModalViewModel homeModalViewModel = new(invoiceDetailDto, _productDtos, saveHandler, deleteHandler);
        await PageHelper.Current.Navigation.PushModalAsync(new HomeModalPage(homeModalViewModel));
    }

    private void Add(InvoiceDetailDto invoiceDetailDto) {
        var number = InvoiceDetailDtos.Count + 1;
        invoiceDetailDto.Number = number;
        InvoiceDetailDtos.Add(invoiceDetailDto);
        Total = InvoiceDetailDtos.Sum(x => x.Total);
    }

    private void Update(InvoiceDetailDto invoiceDetailDto) {
        InvoiceDetailDtos[_index] = invoiceDetailDto;
        Total = InvoiceDetailDtos.Sum(x => x.Total);
    }

    private void Delete() {
        InvoiceDetailDtos.RemoveAt(_index);
        if (_index < InvoiceDetailDtos.Count) {
            UpdateNumbers(_index);
        }
    }

    private void UpdateNumbers(int startIndex) {
        for (int i = startIndex; i < InvoiceDetailDtos.Count; i++) {
            var invoiceDetailDto = InvoiceDetailDtos[i];
            invoiceDetailDto.Number = i + 1;
            InvoiceDetailDtos[i] = invoiceDetailDto;
        }
    }

    private async Task ConfirmResetAsync() {
        bool isReset = await PageHelper.Current.DisplayAlert("Подтвердить действие", "Вы хотите все удалить?", "Да", "Нет");

        if (isReset) {
            await ResetAsync();
        }
    }

    private async Task ResetAsync() {
        InvoiceDetailDtos = [];
        OnPropertyChanged(nameof(InvoiceDetailDtos));
        Date = DateTime.Now;
        OnPropertyChanged(nameof(Date));
        Buyer = null;
        Total = 0;
        Discount = null;
        TotalWithDiscount = 0;
        IsPaid = false;
        await LoadAsync();
    }

    private async Task SaveAsync() {
        await ExecuteAsync(async () => {
            _invoiceDto.InvoiceDetailDtos = InvoiceDetailDtos;
            _invoiceDto.Date = DateOnly.FromDateTime(Date);
            await _invoiceApiClient.AddAsync(_invoiceDto.ToModel());
            await ResetAsync();
        });
    }

    private double GetTotalWithDiscount() {
        return Math.Round(Total - (Discount ?? 0));
    }

    private bool IsValid() {
        return Buyer != null && TotalWithDiscount > 0;
    }
}