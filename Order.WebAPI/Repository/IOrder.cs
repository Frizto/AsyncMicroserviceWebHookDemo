using Shared.DTOs;
using Shared.Entities;

namespace OrderWebAPI.Repository;

public interface IOrder
{
    Task<ServiceResponse> AddOrderAsync(Order order);
    Task<List<Order>> GetAllOrdersAsync();
    Task<OrderSummary> GetOrderSummaryAsync();
}
