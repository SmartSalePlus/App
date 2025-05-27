namespace SmartSaleApp.Models.View;

public sealed class ProductDto {
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public int? Count { get; set; }
    public int? CountInPackage { get; set; }
    public double? Price { get; set; }

    public override string ToString() {
        return CountInPackage > 1 ? $"{Name} ({CountInPackage})" : Name;
    }
}