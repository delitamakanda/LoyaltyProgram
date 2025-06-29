using System.Text.Json.Serialization;
using LoyaltyProgram.Domain;

public class LoyaltyCard
{
    [JsonPropertyName("id")]
    public int LoyaltyCardId { get; set; }
    [JsonPropertyName("card_number")]
    public string? CardNumber { get; set; }
    [JsonPropertyName("client")]
    public Client? Client { get; set; }
    [JsonPropertyName("points")]
    public int Points { get; set; }
    [JsonPropertyName("date_created")]
    public DateTime DateCreated { get; set; }
    [JsonPropertyName("status")]
    public LoyaltyCardStatus Status { get; set; }
    [JsonPropertyName("transactions")]
    public List<Transaction>? Transactions { get; set; }
}

public enum LoyaltyCardStatus 
{
    Active,
    Inactive,
    Expired
}