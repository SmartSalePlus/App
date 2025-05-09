namespace SmartSaleApp.Models.View;

public sealed class InvoiceDetailViewModel {
    public int Number { get; set; }
    public int? Count { get; set; }
    public double? Price { get; set; }
    public double Total { get; set; }
    public ProductViewModel? ProductViewModel { get; set; }
}