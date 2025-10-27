using SmartSaleApp.Extensions.Mapping;
using SmartSaleApp.Helpers;
using SmartSaleApp.Models.Data;
using SmartSaleApp.Models.View;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Input;

namespace SmartSaleApp.ViewModels;

public sealed class ProductModalViewModel : INotifyPropertyChanged {
    public event PropertyChangedEventHandler? PropertyChanged;
    public ICommand SaveCommand { get; }

    public string Name {
        get => _productDto.Name;
        set {
            if (_productDto.Name != value) {
                _productDto.Name = value;
                OnPropertyChanged();
                ((Command)SaveCommand).ChangeCanExecute();
            }
        }
    }

    public int? Count {
        get => _productDto.Count;
        set {
            if (_productDto.Count != value) {
                _productDto.Count = value;
                OnPropertyChanged();
                ((Command)SaveCommand).ChangeCanExecute();
            }
        }
    }

    public int? CountInPackage {
        get => _productDto.CountInPackage;
        set {
            if (_productDto.CountInPackage != value) {
                _productDto.CountInPackage = value;
                OnPropertyChanged();
                ((Command)SaveCommand).ChangeCanExecute();
            }
        }
    }

    public double? Price {
        get => _productDto.Price;
        set {
            if (_productDto.Price != value) {
                _productDto.Price = value;
                OnPropertyChanged();
                ((Command)SaveCommand).ChangeCanExecute();
            }
        }
    }

    private readonly ProductDto _productDto;
    private readonly Func<Product, Task> _saveHandler;

    public ProductModalViewModel(
        ProductDto productDto,
        Func<Product, Task> saveHandler
    ) {
        _productDto = productDto;
        _saveHandler = saveHandler;
        SaveCommand = new Command(async () => await SaveAsync(), IsValid);
    }

    private async Task SaveAsync() {
        await PageHelper.Current.Navigation.PopModalAsync();
        await _saveHandler.Invoke(_productDto.ToModel());
    }

    private bool IsValid() {
        return !string.IsNullOrWhiteSpace(Name) && Count > 0 && CountInPackage > 0 && Price > 0;
    }

    private void OnPropertyChanged([CallerMemberName] string prop = "") {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prop));
    }
}