using SmartSaleApp.Models.Data;
using SmartSaleApp.Models.View;

namespace SmartSaleApp.Extensions.Mapping;

internal static class InvoiceDetailExtension {
    public static InvoiceDetail ToModel(this InvoiceDetailViewModel src) {
        ArgumentNullException.ThrowIfNull(src);
        ArgumentNullException.ThrowIfNull(src.ProductViewModel);

        return new(
            src.Count ?? 0,
            src.Price ?? 0,
            src.Total,
            src.ProductViewModel.Id
        );
    }

    public static IEnumerable<InvoiceDetail> ToModel(this IEnumerable<InvoiceDetailViewModel> src) {
        return src.Select(x => x.ToModel());
    }

    public static InvoiceDetailViewModel Clone(this InvoiceDetailViewModel src) {
        return new() {
            Number = src.Number,
            Count = src.Count,
            Price = src.Price,
            Total = src.Total,
            ProductViewModel = src.ProductViewModel
        };
    }
}