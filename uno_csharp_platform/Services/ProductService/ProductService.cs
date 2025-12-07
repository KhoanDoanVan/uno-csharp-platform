using System.Text.Json;
using uno_csharp_platform.Models;

namespace uno_csharp_platform.Services;

public class ProductService : IProductService
{
    private readonly HttpClient _httpClient;

    public ProductService(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public async Task<List<Product>> GetProductsAsync()
    {
        // Mock data for demo - replace with actual API call
        return new List<Product>
        {
            new() { Id = 1, Name = "Laptop Dell XPS 13", Description = "Laptop cao cấp, chip Intel i7", Price = 25000000, ImageUrl = "https://via.placeholder.com/300", Category = "Electronics", Stock = 10 },
            new() { Id = 2, Name = "iPhone 15 Pro", Description = "Điện thoại Apple mới nhất", Price = 30000000, ImageUrl = "https://via.placeholder.com/300", Category = "Electronics", Stock = 15 },
            new() { Id = 3, Name = "Samsung Galaxy S24", Description = "Flagship Samsung 2024", Price = 22000000, ImageUrl = "https://via.placeholder.com/300", Category = "Electronics", Stock = 20 },
            new() { Id = 4, Name = "AirPods Pro", Description = "Tai nghe không dây chống ồn", Price = 6000000, ImageUrl = "https://via.placeholder.com/300", Category = "Accessories", Stock = 30 },
            new() { Id = 5, Name = "iPad Air", Description = "Máy tính bảng Apple", Price = 15000000, ImageUrl = "https://via.placeholder.com/300", Category = "Electronics", Stock = 12 }
        };
    }

    public async Task<Product?> GetProductByIdAsync(int id)
    {
        var products = await GetProductsAsync();
        return products.FirstOrDefault(p => p.Id == id);
    }
}