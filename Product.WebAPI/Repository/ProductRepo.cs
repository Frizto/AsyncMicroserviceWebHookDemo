using MassTransit;
using Microsoft.EntityFrameworkCore;
using ProductWebAPI.Data;
using Shared.DTOs;
using Shared.Entities;

namespace ProductWebAPI.Repository;

public class ProductRepo(ProductDbContext productDbContext, IPublishEndpoint publishEndpoint) : IProduct
{
    public async Task<ServiceResponse> AddProductAsync(Product product)
    {
        productDbContext.Products.Add(product);
        var result = await productDbContext.SaveChangesAsync();
        await publishEndpoint.Publish(product);

        if (result <= 0)
            return new ServiceResponse(false, "Failed to add product", DateTime.Now);

        return new ServiceResponse(true, "Product added successfully", DateTime.Now);
    }

    public async Task<List<Product>> GetAllProductsAsync()
    {
        var result = await productDbContext.Products.ToListAsync();
        return result;
    }

    public Task<ServiceResponse> UpdateProductAsync(Product product)
    {
        throw new NotImplementedException();
    }
}
