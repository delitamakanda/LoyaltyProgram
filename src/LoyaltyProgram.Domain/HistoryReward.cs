using System.Text.Json.Serialization;
using LoyaltyProgram.Domain;

namespace LoyaltyProgram.Domain;

public class HistoryReward
{
    [JsonPropertyName("id")]
    public int HistoryRewardId { get; set; }
    [JsonPropertyName("created_date")]
    public DateTime CreatedDate { get; set; }
    [JsonPropertyName("description")]
    public string? Description { get; set; }
    [JsonPropertyName("reward")]
    public Reward? Reward { get; set; }
    [JsonPropertyName("client")]
    public Client? Client { get; set; }
}