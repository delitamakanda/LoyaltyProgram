using System.Text.Json.Serialization;
using LoyaltyProgram.Domain;

namespace LoyaltyProgram.Domain;

public class Reward
{
    [JsonPropertyName("id")]
    public int RewardId { get; set; }
    [JsonPropertyName("created_date")]
    public DateTime CreatedDate { get; set; }
    [JsonPropertyName("description")]
    public string? Description { get; set; }
    [JsonPropertyName("points_needed")]
    public decimal PointsNeeded { get; set; }
    [JsonPropertyName("stock")]
    public int Stock { get; set; }
}
