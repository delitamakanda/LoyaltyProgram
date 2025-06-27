using LoyaltyProgram.Domain;

public class Transaction
{
    public int TransactionId { get; set; }
    public DateTime CreatedAt { get; set; }
    public decimal Amount { get; set; }
    public int AwardedPoints { get; set; }
    public string? TransactionType { get; set; }
    public Shop? Shop { get; set; }
}

enum TransactionType
{
    Purchase,
    Reward,
    Refund
}