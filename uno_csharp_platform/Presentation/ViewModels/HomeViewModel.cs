using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System.Collections.ObjectModel;
using uno_csharp_platform.Models;
using uno_csharp_platform.Services;

namespace uno_csharp_platform.Presentation.ViewModels;

public partial class HomeViewModel : ObservableObject
{
    private readonly INavigator _navigator;
    private readonly IProductService _productService;
    private readonly ICartService _cartService;

    [ObservableProperty]
    private ObservableCollection<Product> _products = new();

    [ObservableProperty]
    private bool _isLoading;

    [ObservableProperty]
    private int _cartItemCount;

    [ObservableProperty]
    private string _searchQuery = string.Empty;

    public HomeViewModel(
        INavigator navigator,
        IProductService productService,
        ICartService cartService
    )
    {
        _navigator = navigator;
        _productService = productService;
        _cartService = cartService;
    }

    public async Task LoadDataAsync()
    {
        IsLoading = true;
        try
        {
            var products = await _productService.GetProductsAsync();
            Products = new ObservableCollection<Product>(products);
            await UpdateCartCountAsync();
        }
        catch (Exception ex)
        {
            System.Diagnostics.Debug.WriteLine($"Error loading products: {ex.Message}");
        }
        finally
        {
            IsLoading = false;
        }
    }

    [RelayCommand]
    private async Task AddToCartAsync(Product product)
    {
        try
        {
            var cartItem = new CartItem
            {
                ProductId = product.Id,
                ProductName = product.Name,
                Price = product.Price,
                Quantity = 1,
                ImageUrl = product.ImageUrl
            };

            await _cartService.AddItemAsync(cartItem);
            await UpdateCartCountAsync();
        }
        catch (Exception ex)
        {
            System.Diagnostics.Debug.WriteLine($"Error adding to cart: {ex.Message}");
        }
    }

    [RelayCommand]
    private async Task ViewProductDetailsAsync(Product product)
    {
        await _navigator.NavigateViewModelAsync<ProductDetailViewModel>(
            this, 
            data: new Dictionary<string, object> { ["ProductId"] = product.Id }
        );
    }

    [RelayCommand]
    private async Task NavigateToCartAsync()
    {
        await _navigator.NavigateViewModelAsync<CartViewModel>(this);
    }

    [RelayCommand]
    private async Task NavigateToProfileAsync()
    {
        await _navigator.NavigateViewModelAsync<ProfileViewModel>(this);
    }

    private async Task UpdateCartCountAsync()
    {
        CartItemCount = await _cartService.GetCartCountAsync();
    }
}