using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace LumanGameShope.Web.DTOs.User
{
    public sealed class LoginRequest
    {
        [Required(ErrorMessage = "نام کاربری الزامی است.")]
        [JsonPropertyName("UserName")]
        public string UserName { get; set; } = string.Empty;

        [Required(ErrorMessage = "رمز عبور الزامی است.")]
        [JsonPropertyName("Password")]
        public string Password { get; set; } = string.Empty;
    }
}
