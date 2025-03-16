namespace SmartSaleApp.Models;

public sealed record InvoiceDetail(
    int Count,
    double Price,
    double Total,
    Product Product
);