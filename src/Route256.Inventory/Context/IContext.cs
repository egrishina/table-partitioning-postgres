using System.Data;

namespace Route256.Inventory.Context;

public interface IContext
{
    IDbConnection CreateConnection();
}