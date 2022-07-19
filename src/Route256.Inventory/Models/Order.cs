using System.ComponentModel.DataAnnotations.Schema;

namespace Route256.Inventory.Models;

[Table("orders", Schema = "core")]
public class Order
{
    [Column("id")]
    public Guid Id { get; set; }
    [Column("client_id")]
    public long ClientId { get; set; }
    [Column("creation_date")]
    public DateTime CreationDate { get; set; }
    [Column("pickup_date")]
    public DateTime? PickupDate { get; set; }
    [Column("status")]
    public Status Status { get; set; }
    [Column("items_data", TypeName = "json")]
    public Product[] Products { get; set; }
    [Column("warehouse_id")]
    public long WarehouseId { get; set; }
}