using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace uno_csharp_platform.Models;

public class CartItem
{
    [System.ComponentModel.DataAnnotations.Key]
    public int Id { get; set; }
    
    public int ProductId { get; set; }
    public string ProductName { get; set; } = string.Empty;
    public decimal Price { get; set; }
    public int Quantity { get; set; }
    public string ImageUrl { get; set; } = string.Empty;
    
    [NotMapped]
    public decimal Subtotal => Price * Quantity;
}