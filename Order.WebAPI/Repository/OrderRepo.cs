using MassTransit;
using Microsoft.EntityFrameworkCore;
using OrderWebAPI.Data;
using Shared.DTOs;
using Shared.Entities;
using System.Text;

namespace OrderWebAPI.Repository;

public class OrderRepo(OrderDbContext orderDbContext, IPublishEndpoint publishEndpoint) : IOrder
{
    public async Task<ServiceResponse> AddOrderAsync(Order order)
    {
        orderDbContext.Add(order);
        var result = await orderDbContext.SaveChangesAsync();

        var orderSummary = await GetOrderSummaryAsync();
        string emailBody = BuildOrderEmailBody(orderSummary);
        await publishEndpoint.Publish(new EmailDTO("Order Information", emailBody));
        await ClearOrderTable();

        if (result <= 0)
            return new ServiceResponse(true, "Order added successfully", DateTime.Now);

        return new ServiceResponse(false, "Failed to add order", DateTime.Now);
    }

    public async Task<List<Order>> GetAllOrdersAsync()
    {
        return await orderDbContext.Orders.ToListAsync();
    }

    public async Task<OrderSummary> GetOrderSummaryAsync()
    {
        var order = await orderDbContext.Orders.FirstOrDefaultAsync();
        var products = await orderDbContext.Products.ToListAsync();
        var productInfo = products.Find(x => x.Id == order!.ProductId);

        return new OrderSummary
        (
            order.Id,
            productInfo.Id,
            productInfo.Name,
            productInfo.Price,
            order.Quantity,
            productInfo.Price * order.Quantity,
            order.Date
        );
    }

    private string BuildOrderEmailBody(OrderSummary orderSummary)
    {
        var sb = new StringBuilder();
        sb.AppendLine("<h1><strong>Order Information</strong></h1>");
        sb.AppendLine($"<p><strong>Order ID: </strong> {orderSummary.Id}</p>");
        sb.AppendLine("<h2>Order Item: </h2>");

        sb.AppendLine("<ul>");
            sb.AppendLine($"<li><strong>Product Name: </strong> {orderSummary.ProductName}</li>");
            sb.AppendLine($"<li><strong>Product Price: </strong> {orderSummary.ProductPrice}</li>");
            sb.AppendLine($"<li><strong>Order Quantity: </strong> {orderSummary.Quantity}</li>");
            sb.AppendLine($"<li><strong>Total Amount: </strong> {orderSummary.TotalAmount}</li>");
            sb.AppendLine($"<li><strong>Order Date: </strong> {orderSummary.Date.ToFileTimeUtc}</li>");
        sb.AppendLine("</ul>");

        sb.AppendLine("<p><strong>Thank you for your order!</strong></p>");

        return sb.ToString();
    }

    private async Task ClearOrderTable()
    {
        var order = await orderDbContext.Orders.FirstOrDefaultAsync();
        orderDbContext.Orders.Remove(order);
        await orderDbContext.SaveChangesAsync();
    }
}
