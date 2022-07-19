using System.Text.Json.Serialization;
using Dapper;
using Route256.Inventory.Context;
using Route256.Inventory.Models;
using Route256.Inventory.Storage;
using Route256.Inventory.TypeHandlers;
using Z.Dapper.Plus;

namespace Route256.Inventory;

public class Startup
{
    private readonly IConfiguration _configuration;

    public Startup(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    public void ConfigureServices(IServiceCollection services)
    {
        DapperPlusManager.Entity<Order>().Table("core.orders").KeepIdentity(true);
        DapperPlusManager.AddValueConverter(typeof(Product[]), new JsonTypeHandler());
        
        services.AddSingleton<IContext, DapperContext>();
        services.AddSingleton<IOrderStorage, OrderStorage>();
        services.AddControllers()
            .AddJsonOptions(options =>
            {
                options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
            });
    }

    public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
    {
        if (env.IsDevelopment())
        {
            app.UseDeveloperExceptionPage();
        }

        app.UseRouting();

        app.UseEndpoints(endpoints =>
        {
            endpoints.MapGet("/",
                async context => { await context.Response.WriteAsync("Hello World!"); });
            endpoints.MapControllers();
        });
    }
}