using SmartSaleApp.Models.Data;

namespace SmartSaleApp.Models.View;

public sealed class InvoiceDto {
    public int Id { get; set; }
    public DateOnly Date { get; set; }
    public double Total { get; set; }
    public double? Discount { get; set; }
    public double TotalWithDiscount { get; set; }
    public bool IsPaid { get; set; }
    public string Status => IsPaid ? "Оплачено" : "Не оплачено";
    public Buyer? Buyer { get; set; }
    public IEnumerable<InvoiceDetailDto> InvoiceDetailDtos{ get; set; } = [];
}