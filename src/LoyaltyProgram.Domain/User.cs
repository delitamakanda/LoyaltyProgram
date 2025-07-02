using System.Text.Json.Serialization;

namespace LoyaltyProgram.Domain
{
    public class User
    {
        [JsonPropertyName("id")]
        public int UserId { get; set; }
        [JsonPropertyName("username")]
        public required string Username { get; set; }
        [JsonPropertyName("password_hash")]
        public required string PasswordHash { get; set; }
        [JsonPropertyName("role")]
        public required string Role { get; set; }
    }
}