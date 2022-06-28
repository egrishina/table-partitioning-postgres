namespace Route256.Inventory.Models;

public record OrderDto
(
    Guid Id,
    long ClientId,
    DateTime CreationDate,
    DateTime PickupDate,
    Status Status,
    Product[] Products,
    long WarehouseId
);
