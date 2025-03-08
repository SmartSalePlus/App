namespace SmartSaleApp.Models;

public sealed record ProductPriceHistory(
    double Price,
    DateOnly DateBegin,
    DateOnly DateEnd
);