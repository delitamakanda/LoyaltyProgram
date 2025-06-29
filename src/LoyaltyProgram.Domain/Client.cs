using System.Text.Json.Serialization;
using LoyaltyProgram.Domain;

public class Client
{
    [JsonPropertyName("id")]
    public int ClientId { get; set; }
    [JsonPropertyName("first_name")]
    public string? FirstName { get; set; }
    [JsonPropertyName("last_name")]
    public string? LastName { get; set; }
    [JsonPropertyName("address")]
    public string? Address { get; set; }
    [JsonPropertyName("email")]
    public string? Email { get; set; }
    [JsonPropertyName("phone_number")]
    public string? PhoneNumber { get; set; }
    [JsonPropertyName("date_of_birth")]
    public DateTime DateOfBirth { get; set; }
    [JsonPropertyName("date_created")]
    public DateTime? DateCreated { get; set; }
    [JsonPropertyName("loyalty_card")]
    public LoyaltyCard? LoyaltyCard { get; set; }
}