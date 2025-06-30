using LoyaltyProgram.Api.Controllers;
using LoyaltyProgram.Application;
using LoyaltyProgram.Domain;
using LoyaltyProgram.Infrastructure;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace LoyaltyProgram.Tests;

public class ControllersTests
{
    private static LoyaltyDbContext CreateContext()
    {
        var options = new DbContextOptionsBuilder<LoyaltyDbContext>()
            .UseInMemoryDatabase(Guid.NewGuid().ToString())
            .Options;
        return new LoyaltyDbContext(options);
    }

    [Fact]
    public void GetClient_ReturnsClient_WhenExists()
    {
        using var context = CreateContext();
        var service = new ClientService(context);
        var controller = new ClientsController(service);
        var client = new Client
        {
            FirstName = "Jane",
            LastName = "Doe",
            Address = "1 Way",
            Email = "jane@example.com",
            PhoneNumber = "111"
        };
        service.RegisterClient(client);

        var result = controller.GetClient(client.ClientId);

        var ok = Assert.IsType<OkObjectResult>(result.Result);
        var returned = Assert.IsType<Client>(ok.Value);
        Assert.Equal(client.ClientId, returned.ClientId);
    }

    [Fact]
    public void GetClient_ReturnsNotFound_WhenMissing()
    {
        using var context = CreateContext();
        var controller = new ClientsController(new ClientService(context));

        var result = controller.GetClient(42);

        Assert.IsType<NotFoundResult>(result.Result);
    }

    [Fact]
    public void CreateClient_ReturnsCreatedAndPersists()
    {
        using var context = CreateContext();
        var service = new ClientService(context);
        var controller = new ClientsController(service);
        var client = new Client
        {
            FirstName = "Jim",
            LastName = "Beam",
            Address = "Bourbon St",
            Email = "jim@example.com",
            PhoneNumber = "222"
        };

        var result = controller.CreateClient(client);

        var created = Assert.IsType<CreatedAtActionResult>(result.Result);
        var returned = Assert.IsType<Client>(created.Value);
        Assert.Equal(client.ClientId, returned.ClientId);
        Assert.Single(context.Clients);
    }

    [Fact]
    public void CreateShop_ReturnsCreatedAndPersists()
    {
        using var context = CreateContext();
        var service = new ShopService(context);
        var controller = new ShopsController(service);
        var shop = new Shop { Name = "Super Shop", Address = "City" };

        var result = controller.CreateShop(shop);

        Assert.IsType<CreatedAtActionResult>(result.Result);
        Assert.Single(context.Shops);
    }

    [Fact]
    public void GetShop_ReturnsNotFound_WhenMissing()
    {
        using var context = CreateContext();
        var controller = new ShopsController(new ShopService(context));

        var result = controller.GetShop(99);

        Assert.IsType<NotFoundResult>(result.Result);
    }
}
