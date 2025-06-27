using LoyaltyProgram.Domain;

public class LoyaltyCard
{
    public int LoyaltyCardId { get; set; }
    public string? CardNumber { get; set; }
    public int Points { get; set; }
    public DateTime DateCreated { get; set; }
    public string? Status { get; set; }
}