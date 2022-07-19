using System.Text.Json;
using Dapper;
using Route256.Inventory.Context;
using Route256.Inventory.Models;
using Z.Dapper.Plus;

namespace Route256.Inventory.Storage;

public class OrderStorage : IOrderStorage
{
    private readonly IContext _context;

    public OrderStorage(IContext context)
    {
        _context = context;
    }

    public async IAsyncEnumerable<Order> FindOrdersByFilter(long warehouseId, Status status, DateTime from,
        DateTime to)
    {
        var statusIndex = (int)status;
        using var connection = _context.CreateConnection();
        var parameters = new
            { WarehouseId = warehouseId, StatusIndex = statusIndex, StartDate = from, EndDate = to };
        var query = @"
                SELECT * FROM core.orders
                WHERE warehouse_id = @WarehouseId AND status = @StatusIndex AND (creation_date BETWEEN @StartDate AND @EndDate)";

        using var reader = await connection.ExecuteReaderAsync(query, parameters);
        while (reader.Read())
        {
            var order = new Order
            {
                Id = reader.GetGuid(0),
                ClientId = reader.GetInt64(1),
                CreationDate = reader.GetDateTime(2),
                PickupDate = reader.GetValue(3) == DBNull.Value ? null : reader.GetDateTime(3),
                Status = (Status)reader.GetInt32(4),
                Products = reader.GetValue(5) == DBNull.Value
                    ? null
                    : JsonSerializer.Deserialize<Product[]>(reader.GetString(5)),
                WarehouseId = reader.GetInt64(6)
            };

            yield return order;
        }
    }

    public async Task<bool> CreateOrders(IEnumerable<Order> orders)
    {
        using var connection = _context.CreateConnection();
        await Task.Run(() => connection.BulkInsert(orders));
        return true;
    }
}