namespace uno_csharp_platform.Models;


public record Product
{
    public int ProductId { get; set; }
    public int CategoryId { get; set; }
    public int SupplierId { get; set; }
    public string ProductName { get; set; } = string.Empty;
    public string Barcode { get; set; } = string.Empty;
    public decimal Price { get; set; }
    public string Unit { get; set; } = "pcs";
    public DateTime CreatedAt { get; set; }

    // Navigation properties
    public string? CategoryName { get; set; }
    public int? StockQuantity { get; set; }
    public string? ImageUrl { get; set; }
}