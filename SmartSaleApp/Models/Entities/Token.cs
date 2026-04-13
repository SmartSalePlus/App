using SQLite;

namespace SmartSaleApp.Models.Entities;

public sealed class Token {
    [PrimaryKey]
    public int Id { get; set; }
    public string Value { get; set; } = string.Empty;
}