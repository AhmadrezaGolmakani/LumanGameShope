using System.Net;
using System.Net.Http.Json;
using LumanGameShope.Web.DTOs.User;

namespace LumanGameShope.Web.Services
{
    public sealed record LoginResult(bool Success, string? Error = null);

    public sealed class AuthService
    {
        private readonly HttpClient _httpClient;
        private readonly JwtAuthenticationStateProvider _auth;

        public AuthService(HttpClient httpClient, JwtAuthenticationStateProvider auth)
        {
            _httpClient = httpClient;
            _auth = auth;
        }

        public async Task<LoginResult> LoginAsync(
            LoginRequest request, CancellationToken ct = default)
        {
            using var response = await _httpClient.PostAsJsonAsync(
                "api/v1/Account/Login", request, ct);

            if (response.StatusCode is HttpStatusCode.Unauthorized
                        or HttpStatusCode.BadRequest
                        or HttpStatusCode.NotFound)
                return new(false, "نام کاربری یا رمز عبور اشتباه است.");

            if (!response.IsSuccessStatusCode)
                return new(false, "خطای سرور. کمی بعد دوباره تلاش کن.");

            var result = await response.Content
                .ReadFromJsonAsync<LoginResponse>(cancellationToken: ct);

            if (result is null || string.IsNullOrWhiteSpace(result.JwtToken))
                return new(false, "پاسخ نامعتبر از سرور.");

            await _auth.SignInAsync(result.JwtToken);
            return new(true);
        }

        public Task LogoutAsync() => _auth.SignOutAsync();
    }
}