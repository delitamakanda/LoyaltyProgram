using System.Text.Json.Serialization;

namespace LoyaltyProgram.Domain;

public class RankSystem
{
    [JsonPropertyName("id")]
    public int RankSystemId { get; set; }
    [JsonPropertyName("shop_id")]
    public int ShopId { get; set; }
    [JsonPropertyName("shop")]
    public Shop? Shop { get; set; }
    [JsonPropertyName("rank")]
    public RankStatus Rank { get; set; }
    [JsonPropertyName("points_needed")]
    public int PointsNeeded { get; set; }
    [JsonPropertyName("description")]
    public string? RankDescription { get; set; }
}

public class Shop
{
    [JsonPropertyName("id")]
    public int ShopId { get; set; }
    [JsonPropertyName("name")]
    public string? Name { get; set; }
    [JsonPropertyName("address")]
    public string? Address { get; set; }
    [JsonPropertyName("rank_system")]
    public List<RankSystem> RankSystem { get; set; } = new List<RankSystem>();
}
