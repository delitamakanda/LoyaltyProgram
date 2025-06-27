using LoyaltyProgram.Domain;

public class Reward
{
    public int RewardId { get; set; }
    public string? Description { get; set; }
    public decimal PointsNeeded { get; set; }
    public int Stock { get; set; }
}