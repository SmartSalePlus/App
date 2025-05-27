using SmartSaleApp.Models.Data;
using SmartSaleApp.Models.View;

namespace SmartSaleApp.Extensions.Mapping;
internal static class ProductExtension {
    public static Product ToModel(this ProductDto src) {
        ArgumentNullException.ThrowIfNull(src);

        return new(
            src.Id,
            src.Name,
            src.Count ?? 0,
            src.CountInPackage ?? 0,
            src.Price ?? 0
        );
    }

    public static ProductDto ToDto(this Product src) {
        return new() {
            Id = src.Id,
            Name = src.Name,
            Count = src.Count,
            CountInPackage = src.CountInPackage,
            Price = src.Price
        };
    }

    public static IEnumerable<ProductDto> ToDto(this IEnumerable<Product> src) {
        return src.Select(ToDto);
    }

    public static ProductDto Clone(this ProductDto src) {
        ArgumentNullException.ThrowIfNull(src);

        return new() {
            Id = src.Id,
            Name = src.Name,
            Count = src.Count,
            CountInPackage = src.CountInPackage,
            Price = src.Price
        };
    }
}