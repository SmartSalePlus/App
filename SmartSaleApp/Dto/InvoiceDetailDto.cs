using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace SmartSaleApp.Dto;

public sealed class InvoiceDetailDto : INotifyPropertyChanged {
    public event PropertyChangedEventHandler? PropertyChanged;

    public int Number {
        get => _number;
        set {
            if (_number != value) {
                _number = value;
                OnPropertyChanged();
            }
        }
    }

    public int? Count {
        get => _count;
        set {
            if (_count != value) {
                _count = value;
                OnPropertyChanged();
            }
        }
    }

    public double? Price {
        get => _price;
        set {
            if (_price != value) {
                _price = value;
                OnPropertyChanged();
            }
        }
    }

    public double? Total {
        get => _total;
        set {
            if (_total != value) {
                _total = value;
                OnPropertyChanged();
            }
        }
    }

    public ProductDto? ProductDto {
        get => _productDto;
        set {
            if (_productDto != value) {
                _productDto = value;
                OnPropertyChanged();
            }
        }
    }

    private int _number;
    private int? _count;
    private double? _price;
    private double? _total;
    private ProductDto? _productDto;

    private void OnPropertyChanged([CallerMemberName] string prop = "") {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prop));
    }
}