using System.Data;
using Npgsql;

namespace Route256.Inventory.Context;

public class DapperContext : IContext
{
    private readonly string _connectionString;
    
    public DapperContext(IConfiguration configuration)
    {
        _connectionString = configuration.GetConnectionString("PostgresConnectionString");
    }
    
    public IDbConnection CreateConnection() => new NpgsqlConnection(_connectionString);
}