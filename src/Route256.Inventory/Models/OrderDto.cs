namespace Route256.Inventory.Models;

public class OrderDto
{
    public Guid Id { get; set; }
    public long ClientId { get; set; }
    public DateTime CreationDate { get; set; }
    public DateTime? PickupDate { get; set; }
    public Status Status { get; set; }
    public Product[] Products { get; set; }
    public long WarehouseId { get; set; }
}
