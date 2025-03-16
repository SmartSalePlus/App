using SmartSaleApp.Dto;
using SmartSaleApp.Models;

namespace SmartSaleApp.Extensions;

public static class ProductExtension {
    public static Product ToModel(this ProductDto productDto) {
        ArgumentNullException.ThrowIfNull(productDto);

        return new(
            productDto.Id,
            productDto.Name,
            productDto.Count,
            productDto.CountInPackage,
            productDto.Price,
            productDto.ProductPriceHistories
        );
    }

    public static ProductDto ToDto(this Product product)
        => new(
            product.Id,
            product.Name,
            product.Count,
            product.CountInPackage,
            product.Price,
            product.ProductPriceHistories,
            product.CountInPackage > 1 ? $"{product.Name} ({product.CountInPackage})" : product.Name
        );

    public static IEnumerable<ProductDto> ToDto(this IEnumerable<Product> products)
        => products.Select(x => x.ToDto());
}