using SmartSaleApp.Models;

namespace SmartSaleApp.Dto;

public sealed class InvoiceDto {
    public int Id { get; set; }
    public DateOnly Date { get; set; }
    public double? Total { get; set; }
    public double? Discount { get; set; }
    public double? TotalWithDiscount { get; set; }
    public bool IsPaid { get; set; }
    public Buyer? Buyer { get; set; }
    public IEnumerable<InvoiceDetail> InvoiceDetails { get; set; } = [];
}