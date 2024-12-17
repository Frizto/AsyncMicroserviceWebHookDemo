using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ProductWebAPI.Repository;
using Shared.DTOs;
using Shared.Entities;

namespace ProductWebAPI;
[Route("api/[controller]")]
[ApiController]
public class ProductController(IProduct productInterface) : ControllerBase
{
    [HttpPost("add-product")]
    public async Task<ActionResult<ServiceResponse>> AddProductAsync(Product product)
    {
        var response = await productInterface.AddProductAsync(product);
        return response.Success
            ? Ok(response)
            : BadRequest(response);
    }

    [HttpGet("get-all-products")]
    public async Task<ActionResult<List<Product>>> GetAllProductsAsync()
    {
        var response = await productInterface.GetAllProductsAsync();
        return Ok(response);
    }
}
