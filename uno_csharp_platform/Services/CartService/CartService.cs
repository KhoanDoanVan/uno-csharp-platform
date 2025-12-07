using Microsoft.EntityFrameworkCore;
using uno_csharp_platform.Data;
using uno_csharp_platform.Models;

namespace uno_csharp_platform.Services;

public class CartService : ICartService
{
    private readonly AppDbContext _context;

    public CartService()
    {
        _context = new AppDbContext();
        _context.Database.EnsureCreated();
    }

    public async Task<List<CartItem>> GetAllItemsAsync()
    {
        return await _context.CartItems.ToListAsync();
    }

    public async Task<CartItem?> GetItemByProductIdAsync(int productId)
    {
        return await _context.CartItems
            .FirstOrDefaultAsync(c => c.ProductId == productId);
    }

    public async Task AddItemAsync(CartItem item)
    {
        var existing = await GetItemByProductIdAsync(item.ProductId);
        
        if (existing != null)
        {
            existing.Quantity += item.Quantity;
            await UpdateItemAsync(existing);
        }
        else
        {
            _context.CartItems.Add(item);
            await _context.SaveChangesAsync();
        }
    }

    public async Task UpdateItemAsync(CartItem item)
    {
        _context.CartItems.Update(item);
        await _context.SaveChangesAsync();
    }

    public async Task RemoveItemAsync(int id)
    {
        var item = await _context.CartItems.FindAsync(id);
        if (item != null)
        {
            _context.CartItems.Remove(item);
            await _context.SaveChangesAsync();
        }
    }

    public async Task ClearCartAsync()
    {
        await _context.Database.ExecuteSqlRawAsync("DELETE FROM CartItems");
        await _context.SaveChangesAsync();
    }

    public async Task<decimal> GetTotalPriceAsync()
    {
        var items = await GetAllItemsAsync();
        return items.Sum(i => i.Subtotal);
    }

    public async Task<int> GetCartCountAsync()
    {
        return await _context.CartItems.SumAsync(c => c.Quantity);
    }
}