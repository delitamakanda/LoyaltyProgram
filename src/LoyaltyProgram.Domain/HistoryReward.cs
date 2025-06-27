using LoyaltyProgram.Domain;

public class HistoryReward
{
    public int HistoryRewardId { get; set; }
    public DateTime CreatedDate { get; set; }
    public Reward? Reward { get; set; }
    public Client? Client { get; set; }
}