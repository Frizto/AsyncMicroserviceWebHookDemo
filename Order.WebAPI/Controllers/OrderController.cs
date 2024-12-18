using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OrderWebAPI.Repository;
using Shared.DTOs;
using Shared.Entities;

namespace OrderWebAPI.Controllers;
[Route("api/[controller]")]
[ApiController]
public class OrderController(IOrder orderInterface) : ControllerBase
{
    [HttpPost("add-order")]
    public async Task<ActionResult<ServiceResponse>> AddOrder(Order order)
    {
        var response = await orderInterface.AddOrderAsync(order);
        return response.Success ? Ok(response) : BadRequest(response);
    }
}
