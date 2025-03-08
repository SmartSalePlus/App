namespace SmartSaleApp.Models;

public sealed class InvoiceDetail {
    public int? Count { get; set; }
    public double? Price { get; set; }
    public double? Total { get; set; }
    public Product? Product { get; set; }
}