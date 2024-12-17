using Shared.DTOs;
using Shared.Entities;

namespace ProductWebAPI.Repository;

public interface IProduct
{
    Task<ServiceResponse> AddProductAsync(Product product);
    Task<ServiceResponse> UpdateProductAsync(Product product);

    Task<List<Product>> GetAllProductsAsync();
}
