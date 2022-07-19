using Microsoft.AspNetCore.Mvc;
using Route256.Inventory.Models;
using Route256.Inventory.Storage;

namespace Route256.Inventory.Controllers;

[Route("orders")]
public class OrdersController : Controller
{
    private readonly IOrderStorage _storage;

    public OrdersController(IOrderStorage storage)
    {
        _storage = storage;
    }

    [HttpGet] // https://localhost:7179/orders?warehouseId=1&status=New&from=2022-06-28T00:00:00&to=2022-07-28T00:00:00
    public async IAsyncEnumerable<OrderDto> FindOrdersByFilter([FromQuery] long warehouseId, [FromQuery] Status status,
        [FromQuery] DateTime from, [FromQuery] DateTime to)
    {
        var orders = _storage.FindOrdersByFilter(warehouseId, status, from, to);
        await foreach (var order in orders)
        {
            var orderDto = new OrderDto
            {
                Id = order.Id,
                ClientId = order.ClientId,
                CreationDate = order.CreationDate,
                PickupDate = order.PickupDate,
                Status = order.Status,
                Products = order.Products,
                WarehouseId = order.WarehouseId
            };
            
            yield return orderDto;
        }
    }

    [HttpPost] // https://localhost:7179/orders
    public async Task<ActionResult> CreateOrders([FromBody] CreateOrderDto[] ordersDto)
    {
        var orders = ordersDto.Select(o => new Order
        {
            Id = Guid.NewGuid(),
            ClientId = o.ClientId,
            CreationDate = DateTime.UtcNow,
            PickupDate = null,
            Status = o.Status,
            Products = o.Products,
            WarehouseId = o.WarehouseId
        });

        try
        {
            await _storage.CreateOrders(orders);
        }
        catch (Exception e)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, e.Message);
        }

        return NoContent();
    }
}