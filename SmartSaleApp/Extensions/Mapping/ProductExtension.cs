using SmartSaleApp.Models.Data;
using SmartSaleApp.Models.View;

namespace SmartSaleApp.Extensions.Mapping;
internal static class ProductExtension {
    public static ProductDto ToDto(this Product product) {
        return new() {
            Id = product.Id,
            Name = product.Name,
            Count = product.Count,
            CountInPackage = product.CountInPackage,
            Price = product.Price
        };
    }

    public static IEnumerable<ProductDto> ToDto(this IEnumerable<Product> products) {
        return products.Select(ToDto);
    }
}