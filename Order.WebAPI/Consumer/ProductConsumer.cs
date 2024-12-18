using MassTransit;
using OrderWebAPI.Data;
using Shared.Entities;

namespace OrderWebAPI.Consumer;

public class ProductConsumer(OrderDbContext orderDbContext) : IConsumer<Product>
{
    public async Task Consume(ConsumeContext<Product> transitContext)
    {
        orderDbContext.Products.Add(transitContext.Message);
        await orderDbContext.SaveChangesAsync();
    }
}
