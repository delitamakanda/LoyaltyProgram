using System.Text.Json.Serialization;
using LoyaltyProgram.Domain;

namespace LoyaltyProgram.Domain;

public class Transaction
{
    [JsonPropertyName("id")]
    public int TransactionId { get; set; }
    [JsonPropertyName("created_at")]
    public DateTime CreatedAt { get; set; }
    [JsonPropertyName("amount")]
    public decimal Amount { get; set; }
    [JsonPropertyName("points_awarded")]
    public int AwardedPoints { get; set; }
    [JsonPropertyName("transaction_type")]
    public TransactionType TransactionType { get; set; } = TransactionType.Purchase;
    [JsonPropertyName("shop")]
    public Shop? Shop { get; set; }

    [JsonPropertyName("loyalty_card")]
    public LoyaltyCard? LoyaltyCard { get; set; }
    [JsonPropertyName("date_expiration_points")]
    public DateTime? DateExpirationPoints { get; set; }
    [JsonPropertyName("loyalty_card_id")]
    public string? LoyaltyCardId { get; set; }
}

public enum TransactionType
{
    Purchase,
    Reward,
    Refund
}
