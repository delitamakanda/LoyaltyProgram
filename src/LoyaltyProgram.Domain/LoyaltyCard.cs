using System.Text.Json.Serialization;
using LoyaltyProgram.Domain;

public class LoyaltyCard
{
    [JsonPropertyName("id")]
    public int LoyaltyCardId { get; set; }
    [JsonPropertyName("card_number")]
    public string? CardNumber { get; set; }
    [JsonPropertyName("client")]
    public Client? Client { get; set; } = new Client();
    [JsonPropertyName("points")]
    public int Points { get; set; }
    [JsonPropertyName("date_created")]
    public DateTime DateCreated { get; set; }
    [JsonPropertyName("status")]
    public LoyaltyCardStatus Status { get; set; }
    [JsonPropertyName("transactions")]
    public List<Transaction>? Transactions { get; set; } = new List<Transaction>();

    public void UpdatePoints(int points)
    {
        Points += points;
    }

    public void AddTransaction(Transaction transaction)
    {
        Transactions ??= new List<Transaction>();
        Transactions.Add(transaction);
    }

    public bool IsEligibleForReward(Reward reward)
    {
        return Points >= reward.PointsNeeded;
    }
}

public enum LoyaltyCardStatus 
{
    Active,
    Inactive,
    Expired
}