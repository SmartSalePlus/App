namespace SmartSaleApp.Models;

public sealed record ReceptionDetail(
    int Count,
    double Price,
    Product Product
);