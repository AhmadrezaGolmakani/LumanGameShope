using System.Text.Json.Serialization;

namespace LumanGameShope.Web.DTOs.User
{
    public sealed class LoginResponse
    {
        [JsonPropertyName("Message")]
        public string Message { get; set; } = string.Empty;

        [JsonPropertyName("UserId")]
        public int UserId { get; set; }

        [JsonPropertyName("UserName")]
        public string UserName { get; set; } = string.Empty;

        [JsonPropertyName("JwtToken")]
        public string JwtToken { get; set; } = string.Empty;
    }
}
