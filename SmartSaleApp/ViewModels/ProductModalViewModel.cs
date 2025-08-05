using SmartSaleApp.Extensions.Mapping;
using SmartSaleApp.Interfaces.ApiClients;
using SmartSaleApp.Models.View;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Input;

namespace SmartSaleApp.ViewModels;

public sealed class ProductModalViewModel : INotifyPropertyChanged {
    public event PropertyChangedEventHandler? PropertyChanged;
    public event Action? Saved;
    public ICommand SaveCommand { get; }
    public string Title => _isAdd ? "Добавление товара" : "Редактирование товара";

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

    private readonly IProductApiClient _productApiClient;
    private readonly INavigation _navigation;
    private readonly ProductDto _productDto;
    private readonly bool _isAdd;

    public ProductModalViewModel(
        IProductApiClient productApiClient,
        INavigation navigation,
        ProductDto productDto,
        bool isAdd
    ) {
        _productApiClient = productApiClient;
        _navigation = navigation;
        _productDto = productDto;
        _isAdd = isAdd;
        SaveCommand = new Command(async () => await SaveAsync(), IsValid);
    }

    private async Task SaveAsync() {
        if (_isAdd) {
            await _productApiClient.AddAsync(_productDto.ToModel());
        }
        else {
            await _productApiClient.UpdateAsync(_productDto.ToModel());
        }
        Saved?.Invoke();
        await _navigation.PopModalAsync();
    }

    private bool IsValid() {
        return !string.IsNullOrWhiteSpace(Name) && Count > 0 && CountInPackage > 0 && Price > 0;
    }

    private void OnPropertyChanged([CallerMemberName] string prop = "") {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prop));
    }
}