namespace Route256.Inventory.Models;

public record CreateOrderDto
(
    long ClientId,
    Status Status,
    Product[] Products,
    long WarehouseId
);