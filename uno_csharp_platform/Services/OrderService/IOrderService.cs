namespace uno_csharp_platform.Services;

public interface IOrderService
{
    Task<bool> CreateOrderAsync(OrderModel order);
}