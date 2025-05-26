using SmartSaleApp.Models.Data;
using SmartSaleApp.Models.View;

namespace SmartSaleApp.Extensions.Mapping;

internal static class InvoiceDetailExtension {
    public static InvoiceDetail ToModel(this InvoiceDetailDto src) {
        ArgumentNullException.ThrowIfNull(src);
        ArgumentNullException.ThrowIfNull(src.ProductDto);

        return new(
            src.Count ?? 0,
            src.Price ?? 0,
            src.Total,
            src.ProductDto.Id
        );
    }

    public static IEnumerable<InvoiceDetail> ToModel(this IEnumerable<InvoiceDetailDto> src) {
        return src.Select(x => x.ToModel());
    }

    public static InvoiceDetailDto Clone(this InvoiceDetailDto src) {
        return new() {
            Number = src.Number,
            Count = src.Count,
            Price = src.Price,
            Total = src.Total,
            ProductDto = src.ProductDto
        };
    }
}