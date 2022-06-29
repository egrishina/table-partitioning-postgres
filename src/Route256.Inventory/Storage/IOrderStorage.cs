using Route256.Inventory.Models;

namespace Route256.Inventory.Storage;

public interface IOrderStorage
{
    IAsyncEnumerable<Order> FindOrdersByFilter(long warehouseId, Status status, DateTime from, DateTime to);
    Task<bool> CreateOrders(Order[] orders);
}