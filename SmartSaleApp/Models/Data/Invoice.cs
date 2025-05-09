namespace SmartSaleApp.Models.Data;

public sealed record Invoice(
    int Id,
    DateOnly Date,
    double Total,
    double Discount,
    double TotalWithDiscount,
    bool IsPaid,
    int BuyerId,
    IEnumerable<InvoiceDetail> InvoiceDetails
);