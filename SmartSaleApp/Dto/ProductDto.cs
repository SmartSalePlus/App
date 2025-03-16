using SmartSaleApp.Models;

namespace SmartSaleApp.Dto;

public sealed record ProductDto(
    int Id,
    string Name,
    int Count,
    int CountInPackage,
    double Price,
    IEnumerable<ProductPriceHistory> ProductPriceHistories,
    string NameWithCountInPackage
);