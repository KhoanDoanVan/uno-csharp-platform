using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System.Collections.ObjectModel;
using uno_csharp_platform.Models;
using uno_csharp_platform.Services;

namespace uno_csharp_platform.Presentation.ViewModels;

public partial class CartViewModel : ObservableObject
{
    private readonly INavigator _navigator;
    // private readonly ICartService _cartService;

    [ObservableProperty]
    private ObservableCollection<CartItem> _cartItems = new();

    [ObservableProperty]
    private decimal _totalPrice;

    [ObservableProperty]
    private bool _isLoading;

    [ObservableProperty]
    private bool _isEmpty = true;

    public CartViewModel(
        INavigator navigator
        // ICartService cartService
    )
    {
        _navigator = navigator;
        // _cartService = cartService;
    }

    public async Task LoadCartAsync()
    {
        IsLoading = true;
        try
        {
            // var items = await _cartService.GetAllItemsAsync();
            // CartItems = new ObservableCollection<CartItem>(items);
            await UpdateTotalAsync();
            IsEmpty = CartItems.Count == 0;
        }
        catch (Exception ex)
        {
            System.Diagnostics.Debug.WriteLine($"Error loading cart: {ex.Message}");
        }
        finally
        {
            IsLoading = false;
        }
    }

    [RelayCommand]
    private async Task IncreaseQuantityAsync(CartItem item)
    {
        item.Quantity++;
        // await _cartService.UpdateItemAsync(item);
        await UpdateTotalAsync();
    }

    [RelayCommand]
    private async Task DecreaseQuantityAsync(CartItem item)
    {
        if (item.Quantity > 1)
        {
            item.Quantity--;
            // await _cartService.UpdateItemAsync(item);
            await UpdateTotalAsync();
        }
    }

    [RelayCommand]
    private async Task RemoveItemAsync(CartItem item)
    {
        // await _cartService.RemoveItemAsync(item.Id);
        CartItems.Remove(item);
        await UpdateTotalAsync();
        IsEmpty = CartItems.Count == 0;
    }

    [RelayCommand]
    private async Task ClearCartAsync()
    {
        // await _cartService.ClearCartAsync();
        CartItems.Clear();
        await UpdateTotalAsync();
        IsEmpty = true;
    }

    [RelayCommand]
    private async Task ProceedToCheckoutAsync()
    {
        if (CartItems.Count == 0) return;
        // await _navigator.NavigateViewModelAsync<CheckoutViewModel>(this);
    }

    [RelayCommand]
    private async Task NavigateBackAsync()
    {
        await _navigator.NavigateBackAsync(this);
    }

    private async Task UpdateTotalAsync()
    {
        // TotalPrice = await _cartService.GetTotalPriceAsync();
        OnPropertyChanged(nameof(CartItems));
    }
}