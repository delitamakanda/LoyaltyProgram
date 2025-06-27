using LoyaltyProgram.Application;
using LoyaltyProgram.Domain;

public class LoyaltyProgramService
{
    private readonly ClientService _clientService;

    public LoyaltyProgramService(ClientService clientService)
    {
        _clientService = clientService;
    }

    public void RegisterClient(string firstName, string lastName, string email, string phoneNumber, DateTime dateOfBirth)
    {
        var client = new Client
        {
            FirstName = firstName,
            LastName = lastName,
            Email = email,
            PhoneNumber = phoneNumber,
            DateOfBirth = dateOfBirth,
            DateCreated = DateTime.Now
        };

        _clientService.RegisterClient(client);
    }
    
}