using SmartSaleApp.Models.Data;
using SmartSaleApp.Models.View;

namespace SmartSaleApp.Extensions.Mapping;
internal static class ProductExtension {
    public static ProductViewModel ToViewModel(this Product product) {
        return new() {
            Id = product.Id,
            Name = product.Name,
            Count = product.Count,
            CountInPackage = product.CountInPackage,
            Price = product.Price
        };
    }

    public static IEnumerable<ProductViewModel> ToViewModel(this IEnumerable<Product> products) {
        return products.Select(ToViewModel);
    }
}