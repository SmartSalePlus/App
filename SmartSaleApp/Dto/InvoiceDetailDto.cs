namespace SmartSaleApp.Dto;

public sealed class InvoiceDetailDto {
    public int Number { get; set; }
    public int? Count { get; set; }
    public double? Price { get; set; }
    public double? Total { get; set; }
    public ProductDto? ProductDto { get; set; }
}