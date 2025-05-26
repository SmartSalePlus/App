using SmartSaleApp.Models.Data;
using SmartSaleApp.Models.View;

namespace SmartSaleApp.Extensions.Mapping;

internal static class InvoiceExtension {
    public static Invoice ToModel(this InvoiceDto src) {
        ArgumentNullException.ThrowIfNull(src);
        ArgumentNullException.ThrowIfNull(src.Buyer);

        return new(
            src.Id,
            src.Date,
            src.Total,
            src.Discount ?? 0,
            src.TotalWithDiscount,
            src.IsPaid,
            src.Buyer.Id,
            src.InvoiceDetailDtos.ToModel()
        ); 
    }
}