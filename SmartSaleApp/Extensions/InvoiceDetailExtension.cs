using SmartSaleApp.Dto;
using SmartSaleApp.Models;

namespace SmartSaleApp.Extensions;

public static class InvoiceDetailExtension {
    public static InvoiceDetail ToModel(this InvoiceDetailDto invoiceDetailDto) {
        ArgumentNullException.ThrowIfNull(invoiceDetailDto);
        ArgumentNullException.ThrowIfNull(invoiceDetailDto.ProductDto);

        return new(
            invoiceDetailDto.Count ?? 0,
            invoiceDetailDto.Price ?? 0,
            invoiceDetailDto.Total ?? 0,
            invoiceDetailDto.ProductDto.ToModel()
        );
    }

    public static IEnumerable<InvoiceDetail> ToModel(this IEnumerable<InvoiceDetailDto> invoiceDetailsDto)
        => invoiceDetailsDto.Select(x => x.ToModel());
}