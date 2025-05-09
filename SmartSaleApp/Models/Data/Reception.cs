using SmartSaleApp.Models.Data;

namespace SmartSaleApp.Models;

public sealed record Reception(
    int Id,
    DateOnly Date,
    IEnumerable<ReceptionDetail> ReceptionDetails
);