namespace SmartSaleApp.Models.Data;

public sealed record InvoiceDetail(
    int Count,
    double Price,
    double Total,
    int ProductId
);