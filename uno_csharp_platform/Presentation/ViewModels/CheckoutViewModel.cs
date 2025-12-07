using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using uno_csharp_platform.Models;
using uno_csharp_platform.Services;

namespace uno_csharp_platform.Presentation.ViewModels;

public partial class CheckoutViewModel : ObservableObject
{
    private readonly INavigator _navigator;
    private readonly ICartService _cartService;
    private readonly IOrderService _orderService;

    [ObservableProperty]
    private string _customerName = string.Empty;

    [ObservableProperty]
    private string _address = string.Empty;

    [ObservableProperty]
    private string _phoneNumber = string.Empty;

    [ObservableProperty]
    private decimal _totalAmount;

    [ObservableProperty]
    private bool _isProcessing;

    [ObservableProperty]
    private string _errorMessage = string.Empty;

    public CheckoutViewModel(
        INavigator navigator,
        ICartService cartService,
        IOrderService orderService
    )
    {
        _navigator = navigator;
        _cartService = cartService;
        _orderService = orderService;
    }

    public async Task LoadDataAsync()
    {
        TotalAmount = await _cartService.GetTotalPriceAsync();
    }

    [RelayCommand]
    private async Task PlaceOrderAsync()
    {
        ErrorMessage = string.Empty;

        if (!ValidateInput())
        {
            return;
        }

        IsProcessing = true;

        try
        {
            // Get cart items
            var cartItems = await _cartService.GetAllItemsAsync();

            if (cartItems.Count == 0)
            {
                ErrorMessage = "Giỏ hàng trống";
                return;
            }

            // Create order model
            var order = new OrderModel
            {
                CustomerName = CustomerName,
                Address = Address,
                PhoneNumber = PhoneNumber,
                TotalAmount = TotalAmount,
                Items = cartItems.Select(c => new OrderItem
                {
                    ProductId = c.ProductId,
                    ProductName = c.ProductName,
                    Price = c.Price,
                    Quantity = c.Quantity,
                    Subtotal = c.Subtotal
                }).ToList()
            };

            // Call API to create order
            var success = await _orderService.CreateOrderAsync(order);

            if (success)
            {
                // Clear cart on success
                await _cartService.ClearCartAsync();

                // Navigate to success page or home
                await _navigator.NavigateViewModelAsync<OrderSuccessViewModel>(this);
            }
            else
            {
                ErrorMessage = "Đặt hàng thất bại. Vui lòng thử lại.";
            }
        }
        catch (Exception ex)
        {
            ErrorMessage = $"Lỗi: {ex.Message}";
        }
        finally
        {
            IsProcessing = false;
        }
    }

    [RelayCommand]
    private async Task NavigateBackAsync()
    {
        await _navigator.NavigateBackAsync(this);
    }

    private bool ValidateInput()
    {
        if (string.IsNullOrWhiteSpace(CustomerName))
        {
            ErrorMessage = "Vui lòng nhập tên khách hàng";
            return false;
        }

        if (string.IsNullOrWhiteSpace(Address))
        {
            ErrorMessage = "Vui lòng nhập địa chỉ";
            return false;
        }

        if (string.IsNullOrWhiteSpace(PhoneNumber))
        {
            ErrorMessage = "Vui lòng nhập số điện thoại";
            return false;
        }

        if (PhoneNumber.Length < 10)
        {
            ErrorMessage = "Số điện thoại không hợp lệ";
            return false;
        }

        return true;
    }
}