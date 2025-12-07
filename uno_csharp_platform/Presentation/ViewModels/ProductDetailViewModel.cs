using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using uno_csharp_platform.Models;
using uno_csharp_platform.Services;

namespace uno_csharp_platform.Presentation.ViewModels;

public partial class ProductDetailViewModel : ObservableObject
{
    private readonly INavigator _navigator;
    // private readonly IProductService _productService;
    // private readonly ICartService _cartService;

    [ObservableProperty]
    private Product? _product;

    [ObservableProperty]
    private int _quantity = 1;

    [ObservableProperty]
    private bool _isLoading;

    [ObservableProperty]
    private string _message = string.Empty;

    public ProductDetailViewModel(
        INavigator navigator
        // IProductService productService,
        // ICartService cartService
    )
    {
        _navigator = navigator;
        // _productService = productService;
        // _cartService = cartService;
    }

    public async Task LoadProductAsync(int productId)
    {
        IsLoading = true;
        try
        {
            // Product = await _productService.GetProductByIdAsync(productId);
        }
        catch (Exception ex)
        {
            Message = $"Lỗi: {ex.Message}";
        }
        finally
        {
            IsLoading = false;
        }
    }

    [RelayCommand]
    private void IncreaseQuantity()
    {
        if (Product != null && Quantity < Product.Stock)
        {
            Quantity++;
        }
    }

    [RelayCommand]
    private void DecreaseQuantity()
    {
        if (Quantity > 1)
        {
            Quantity--;
        }
    }

    [RelayCommand]
    private async Task AddToCartAsync()
    {
        if (Product == null) return;

        try
        {
            var cartItem = new CartItem
            {
                ProductId = Product.Id,
                ProductName = Product.Name,
                Price = Product.Price,
                Quantity = Quantity,
                ImageUrl = Product.ImageUrl
            };

            // await _cartService.AddItemAsync(cartItem);
            Message = "Đã thêm vào giỏ hàng!";
            
            await Task.Delay(1500);
            Message = string.Empty;
        }
        catch (Exception ex)
        {
            Message = $"Lỗi: {ex.Message}";
        }
    }

    [RelayCommand]
    private async Task NavigateBackAsync()
    {
        await _navigator.NavigateBackAsync(this);
    }

    [RelayCommand]
    private async Task NavigateToCartAsync()
    {
        // await _navigator.NavigateViewModelAsync<CartViewModel>(this);
    }
}