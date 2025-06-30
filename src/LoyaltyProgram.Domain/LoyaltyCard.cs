using System.Text.Json.Serialization;
using LoyaltyProgram.Domain;

namespace LoyaltyProgram.Domain;

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
    [JsonPropertyName("rank")]
    public RankStatus Rank { get; set; } = RankStatus.Basic;

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

    public int GetValidPoints()
    {
        if (Transactions == null)
        {
            return 0;
        }
        var currentDate = DateTime.UtcNow;
        return Transactions.Where(t => t.DateExpirationPoints == null || t.DateExpirationPoints > currentDate).Sum(t => t.AwardedPoints);
    }
}

public enum LoyaltyCardStatus
    {
        Active,
        Inactive,
        Expired
    }

public enum RankStatus
{
    Basic,
    Silver,
    Gold,
    Platinum
}