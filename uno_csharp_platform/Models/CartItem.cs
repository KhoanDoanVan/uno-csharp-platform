namespace uno_csharp_platform.Models;


public record CartItem
{
    public int CartItemId { get; set; }
    public int ProductId { get; set; }
    public string ProductName { get; set; } = string.Empty;
    public decimal Price { get; set; }
    public int Quantity { get; set; }
    public string Unit { get; set; } = "pcs";
    public decimal Subtotal => Price * Quantity;
    public DateTime AddedAt { get; set; } = DateTime.Now;
}