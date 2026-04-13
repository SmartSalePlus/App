using SmartSaleApp.Interfaces.ApiClients;
using SmartSaleApp.Models.Data;
using SmartSaleApp.Models.InputParameters;
using SmartSaleApp.Models.View;
using System.Collections.ObjectModel;
using System.Windows.Input;

namespace SmartSaleApp.ViewModels;

public sealed class HistoryViewModel : ViewModelBase {
    public ICommand LoadCommand { get; }
    public ICommand SetIsPaidCommand { get; }
    public ObservableCollection<Buyer> Buyers { get; private set; } = [];
    public ObservableCollection<InvoiceDto> InvoiceDtos { get; private set; } = [];

    public DateTime DateBegin {
        get => _dateBegin;
        set {
            if (_dateBegin != value) {
                _dateBegin = value;
                OnPropertyChanged();
                if (_dateBegin > _dateEnd) {
                    DateEnd = _dateBegin;
                }
            }
        }
    }
    public DateTime DateEnd {
        get => _dateEnd;
        set {
            if (_dateEnd != value) {
                _dateEnd = value;
                OnPropertyChanged();
                if (_dateEnd < _dateBegin) {
                    DateBegin = _dateEnd;
                }
            }
        }
    }

    public Buyer Buyer { get; set; }

    public bool IsPaid {
        get => _isPaid;
        set {
            if (_isPaid != value) {
                _isPaid = value;
                OnPropertyChanged();
            }
        }
    }

    private DateTime _dateBegin;
    private DateTime _dateEnd;
    private bool _isPaid;
    private Buyer _defaultBuyer = new(0, "Все");

    private readonly IInvoiceApiClient _invoiceApiClient;
    private readonly IBuyerApiClient _buyerApiClient;

    public HistoryViewModel(IInvoiceApiClient invoiceApiClient, IBuyerApiClient buyerApiClient) {
        _invoiceApiClient = invoiceApiClient;
        _buyerApiClient = buyerApiClient;
        DateBegin = DateTime.Now;
        DateEnd = DateTime.Now;
        Buyers.Add(_defaultBuyer);
        Buyer = Buyers.First();
        LoadCommand = new Command(async () => await ExecuteAsync(GetInvoicesAsync));
        SetIsPaidCommand = new Command(() => IsPaid = !IsPaid);
    }

    public async Task LoadAsync() {
        await ExecuteAsync(async () => {
            await GetBuyersAsync();
            await GetInvoicesAsync();
        });
    }

    private async Task GetBuyersAsync() {
        var buyers = await _buyerApiClient.GetAsync();
        Buyers = new(buyers);
        Buyers.Insert(0, _defaultBuyer);
        OnPropertyChanged(nameof(Buyers));
    }

    private async Task GetInvoicesAsync() {
        var buyer = Buyer ?? _defaultBuyer;
        var parameter = new InvoiceInputParameter(DateOnly.FromDateTime(DateBegin), DateOnly.FromDateTime(DateEnd), buyer.Id, IsPaid);
        var invoices = await _invoiceApiClient.GetAsync(parameter);
        InvoiceDtos = new(invoices);
        OnPropertyChanged(nameof(InvoiceDtos));
    }
}