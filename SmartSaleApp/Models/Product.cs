namespace SmartSaleApp.Models;

public sealed record Product(
    int Id,
    string Name,
    int Count,
    int CountInPackage,
    double Price,
    IEnumerable<ProductPriceHistory> ProductPriceHistories
);