using System.Text.Json;
using Z.BulkOperations;

namespace Route256.Inventory.TypeHandlers;

public class JsonTypeHandler : IBulkValueConverter
{
    public object ConvertFromProvider(Type destinationType, object value)
    {
        if (value is string json)
        {
            return JsonSerializer.Deserialize(json, destinationType);
        }

        return null;
    }

    public object ConvertToProvider(object value)
    {
        return JsonSerializer.Serialize(value);
    }
}