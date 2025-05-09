using SmartSaleApp.Models.Data;

namespace SmartSaleApp.Models.View;

public sealed class InvoiceViewModel {
    public int Id { get; set; }
    public DateTime Date { get; set; }
    public double Total { get; set; }
    public double Discount { get; set; }
    public double TotalWithDiscount { get; set; }
    public bool IsPaid { get; set; }
    public Buyer? Buyer { get; set; }
    public IEnumerable<InvoiceDetailViewModel> InvoiceDetailViewModels { get; set; } = [];
}