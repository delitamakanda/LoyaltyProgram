using System.Text.Json.Serialization;
namespace LoyaltyProgram.Domain
{
    public class LoginRequest
    {
        [JsonPropertyName("username")]
        public required string Username { get; set; }
        [JsonPropertyName("password")]
        public required string Password { get; set; }
    }

    public class RegisterRequest
    {
        [JsonPropertyName("username")]
        public required string Username { get; set; }
        [JsonPropertyName("password")]
        public required string Password { get; set; }
        [JsonPropertyName("role")]
        public required string Role { get; set; }
    }

    public class AuthResponse
    {
        [JsonPropertyName("token")]
        public required string Token { get; set; }
        [JsonPropertyName("username")]
        public required string Username;
    }
}