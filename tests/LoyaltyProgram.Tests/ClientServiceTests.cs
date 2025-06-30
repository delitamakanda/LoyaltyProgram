using LoyaltyProgram.Application;
using LoyaltyProgram.Domain;
using LoyaltyProgram.Infrastructure;
using Microsoft.EntityFrameworkCore;

namespace LoyaltyProgram.Tests;

public class ClientServiceTests
{
    private static LoyaltyDbContext CreateContext()
    {
        var options = new DbContextOptionsBuilder<LoyaltyDbContext>()
            .UseInMemoryDatabase(Guid.NewGuid().ToString())
            .Options;
        return new LoyaltyDbContext(options);
    }

    [Fact]
    public void RegisterClient_AssignsLoyaltyCardAndPersistsClient()
    {
        using var context = CreateContext();
        var service = new ClientService(context);
        var client = new Client
        {
            FirstName = "John",
            LastName = "Doe",
            Address = "123 Street",
            Email = "john@example.com",
            PhoneNumber = "1234567890"
        };

        service.RegisterClient(client);

        Assert.Equal(1, context.Clients.Count());
        Assert.NotNull(client.LoyaltyCard);
        Assert.StartsWith("LOYALTY-", client.LoyaltyCard!.CardNumber);
        Assert.Equal(LoyaltyCardStatus.Active, client.LoyaltyCard.Status);
    }

    [Fact]
    public void RegisterClient_InvalidData_ThrowsArgumentException()
    {
        using var context = CreateContext();
        var service = new ClientService(context);
        var client = new Client { Email = "bad", PhoneNumber = "" };

        Assert.Throws<ArgumentException>(() => service.RegisterClient(client));
        Assert.Empty(context.Clients);
    }
}
