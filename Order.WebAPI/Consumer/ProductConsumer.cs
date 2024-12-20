using MassTransit;
using OrderWebAPI.Data;
using Shared.Entities;

namespace OrderWebAPI.Consumer;

public class ProductConsumer(OrderDbContext orderDbContext, ILogger<ProductConsumer> logger) : IConsumer<Product>
{
    public async Task Consume(ConsumeContext<Product> context)
    {
        try
        {
            var product = context.Message;
            product.Id = 0; // Ensure the Id is not set explicitly

            orderDbContext.Products.Add(product);
            await orderDbContext.SaveChangesAsync();
            logger.LogInformation("Product consumed and saved successfully: {Product}", product);
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Error consuming product: {Product}", context.Message);
            // Handle the exception as needed
        }
    }
}
