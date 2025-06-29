using System.Text.Json.Serialization;

namespace LoyaltyProgram.Domain;

public class Shop
{
    [JsonPropertyName("id")]
    public int ShopId { get; set; }
    [JsonPropertyName("name")]
    public string? Name { get; set; }
    [JsonPropertyName("address")]
    public string? Address { get; set; }
}
