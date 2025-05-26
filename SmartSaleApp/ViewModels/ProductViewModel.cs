using SmartSaleApp.Models.View;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows.Input;

namespace SmartSaleApp.ViewModels;

public sealed class ProductViewModel : INotifyPropertyChanged {
    public event PropertyChangedEventHandler? PropertyChanged;

    public ICommand LoadCommand { get; }
    public ICommand AddCommand { get; }

    public ObservableCollection<ProductDto> ProductDtos { get; private set; } = [];

}