using SmartSaleApp.Models.Data;
using SmartSaleApp.Models.View;

namespace SmartSaleApp.Extensions.Mapping;

internal static class InvoiceExtension {
    public static Invoice ToModel(this InvoiceViewModel src) {
        ArgumentNullException.ThrowIfNull(src);
        ArgumentNullException.ThrowIfNull(src.Buyer);

        return new(
            src.Id,
            DateOnly.FromDateTime(src.Date),
            src.Total,
            src.Discount ?? 0,
            src.TotalWithDiscount,
            src.IsPaid,
            src.Buyer.Id,
            src.InvoiceDetailViewModels.ToModel()
        ); 
    }
}