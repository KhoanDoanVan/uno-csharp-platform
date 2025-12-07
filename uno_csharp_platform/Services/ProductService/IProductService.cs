namespace uno_csharp_platform.Services;

public interface IProductService
{
    Task<List<Product>> GetProductsAsync();
    Task<Product?> GetProductByIdAsync(int id);
}