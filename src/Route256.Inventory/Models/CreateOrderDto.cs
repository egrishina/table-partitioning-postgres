namespace Route256.Inventory.Models;

public class CreateOrderDto
{
    public long ClientId { get; set; }
    public Status Status { get; set; }
    public Product[] Products { get; set; }
    public long WarehouseId { get; set; }
}