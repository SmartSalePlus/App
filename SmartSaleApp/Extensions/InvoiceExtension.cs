using SmartSaleApp.Dto;
using SmartSaleApp.Models;

namespace SmartSaleApp.Extensions;

public static class InvoiceExtension {
    public static Invoice ToModel(this InvoiceDto invoiceDto) {
        ArgumentNullException.ThrowIfNull(invoiceDto);
        ArgumentNullException.ThrowIfNull(invoiceDto.Buyer);

        return new(
            invoiceDto.Id,
            invoiceDto.Date,
            invoiceDto.Total ?? 0,
            invoiceDto.Discount ?? 0,
            invoiceDto.TotalWithDiscount ?? 0,
            invoiceDto.IsPaid,
            invoiceDto.Buyer,
            invoiceDto.InvoiceDetails
        ); 
    }
}