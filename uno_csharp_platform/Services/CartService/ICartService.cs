using uno_csharp_platform.Models;

namespace uno_csharp_platform.Services;

public interface ICartService
{
    Task<List<CartItem>> GetAllItemsAsync();
    Task<CartItem?> GetItemByProductIdAsync(int productId);
    Task AddItemAsync(CartItem item);
    Task UpdateItemAsync(CartItem item);
    Task RemoveItemAsync(int id);
    Task ClearCartAsync();
    Task<decimal> GetTotalPriceAsync();
    Task<int> GetCartCountAsync();
}