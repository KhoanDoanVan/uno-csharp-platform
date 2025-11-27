namespace uno_csharp_platform.Models;


public record Category
{
    public int CategoryId { get; set; }
    public string CategoryName { get; set; } = string.Empty;
}