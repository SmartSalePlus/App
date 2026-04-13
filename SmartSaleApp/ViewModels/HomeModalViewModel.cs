using SmartSaleApp.Helpers;
using SmartSaleApp.Models.View;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Input;

namespace SmartSaleApp.ViewModels;

public sealed class HomeModalViewModel : INotifyPropertyChanged {
    public event PropertyChangedEventHandler? PropertyChanged;
    public ICommand SaveCommand { get; }
    public ICommand DeleteCommand { get; }
    public ObservableCollection<ProductDto> ProductDtos { get; private set; } = [];

    public int? Count {
        get => _invoiceDetailDto.Count;
        set {
            if (_invoiceDetailDto.Count != value) {
                _invoiceDetailDto.Count = value;
                Total = GetTotal();
                OnPropertyChanged();
                ((Command)SaveCommand).ChangeCanExecute();
            }
        }
    }

    public double? Price {
        get => _invoiceDetailDto.Price;
        set {
            if (_invoiceDetailDto.Price != value) {
                _invoiceDetailDto.Price = value;
                Total = GetTotal();
                OnPropertyChanged();
                ((Command)SaveCommand).ChangeCanExecute();
            }
        }
    }

    public double Total {
        get => _invoiceDetailDto.Total;
        set {
            if (_invoiceDetailDto.Total != value) {
                _invoiceDetailDto.Total = value;
                OnPropertyChanged();
            }
        }
    }

    public ProductDto? ProductDto {
        get => _invoiceDetailDto.ProductDto;
        set {
            if (value != null && _invoiceDetailDto.ProductDto != value) {
                _invoiceDetailDto.ProductDto = value;
                Price = value.Price;
                Total = GetTotal();
                OnPropertyChanged();
                ((Command)SaveCommand).ChangeCanExecute();
            }
        }
    }

    public bool IsVisibleDeleteButton => _isEdit;

    private readonly InvoiceDetailDto _invoiceDetailDto;
    private readonly Action<InvoiceDetailDto> _saveHandler;
    private readonly Action? _deleteHandler;
    private bool _isEdit;

    public HomeModalViewModel(
        InvoiceDetailDto invoiceDetailDto,
        IEnumerable<ProductDto> productDtos,
        Action<InvoiceDetailDto> saveHandler,
        Action? deleteHandler
    ) {
        _invoiceDetailDto = invoiceDetailDto;
        ProductDtos = new(productDtos);
        _saveHandler = saveHandler;
        _deleteHandler = deleteHandler;
        _isEdit = deleteHandler != null;
        SaveCommand = new Command(async () => await SaveAsync(), IsValid);
        DeleteCommand = new Command(async () => await DeleteAsync());
        ProductDto = ProductDtos.FirstOrDefault(x => x.Id == _invoiceDetailDto.ProductDto?.Id);
    }

    private async Task SaveAsync() {
        await PageHelper.Current.Navigation.PopModalAsync();
        _saveHandler?.Invoke(_invoiceDetailDto);
    }

    private async Task DeleteAsync() {
        await PageHelper.Current.Navigation.PopModalAsync();
        _deleteHandler?.Invoke();
    }

    private double GetTotal() {
        double total = (ProductDto?.CountInPackage * Count * Price) ?? 0;
        return Math.Round(total);
    }

    private bool IsValid() {
        return ProductDto != null && Count > 0 && Price > 0;
    }

    private void OnPropertyChanged([CallerMemberName] string prop = "") {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prop));
    }
}