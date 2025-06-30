using LoyaltyProgram.Application;
using LoyaltyProgram.Domain;
using LoyaltyProgram.Infrastructure;
using Microsoft.EntityFrameworkCore;

namespace LoyaltyProgram.Tests;

public class ShopServiceTests
{
    private static LoyaltyDbContext CreateContext()
    {
        var options = new DbContextOptionsBuilder<LoyaltyDbContext>()
            .UseInMemoryDatabase(Guid.NewGuid().ToString())
            .Options;
        return new LoyaltyDbContext(options);
    }

    [Fact]
    public void AddShop_PersistsShop()
    {
        using var context = CreateContext();
        var service = new ShopService(context);
        var shop = new Shop { Name = "Shop", Address = "Here" };

        service.AddShop(shop);

        Assert.Equal(1, context.Shops.Count());
        Assert.Contains(shop, context.Shops);
    }
}
