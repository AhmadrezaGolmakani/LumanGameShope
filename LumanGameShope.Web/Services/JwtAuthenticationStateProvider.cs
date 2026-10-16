using System.Security.Claims;
using System.Text.Json;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.JSInterop;

namespace LumanGameShope.Web.Services
{
    public sealed class JwtAuthenticationStateProvider : AuthenticationStateProvider
    {
        private const string TokenKey = "authToken";
        private readonly IJSRuntime _js;

        private static AuthenticationState Anonymous =>
            new(new ClaimsPrincipal(new ClaimsIdentity()));

        public JwtAuthenticationStateProvider(IJSRuntime js) => _js = js;

        public override async Task<AuthenticationState> GetAuthenticationStateAsync()
        {
            var token = await _js.InvokeAsync<string?>(
                "sessionStorage.getItem", TokenKey);

            if (string.IsNullOrWhiteSpace(token))
                return Anonymous;

            var claims = ParseClaims(token);
            if (claims is null) // خراب یا منقضی
            {
                await _js.InvokeVoidAsync("sessionStorage.removeItem", TokenKey);
                return Anonymous;
            }

            var identity = new ClaimsIdentity(
                claims, "jwt", ClaimTypes.Name, ClaimTypes.Role);

            return new AuthenticationState(new ClaimsPrincipal(identity));
        }

        public async Task SignInAsync(string token)
        {
            await _js.InvokeVoidAsync("sessionStorage.setItem", TokenKey, token);
            NotifyAuthenticationStateChanged(GetAuthenticationStateAsync());
        }

        public async Task SignOutAsync()
        {
            await _js.InvokeVoidAsync("sessionStorage.removeItem", TokenKey);
            NotifyAuthenticationStateChanged(GetAuthenticationStateAsync());
        }

        private static List<Claim>? ParseClaims(string jwt)
        {
            try
            {
                var parts = jwt.Split('.');
                if (parts.Length != 3) return null;

                var payload = parts[1].Replace('-', '+').Replace('_', '/');
                payload = payload.PadRight(
                    payload.Length + (4 - payload.Length % 4) % 4, '=');

                using var doc = JsonDocument.Parse(Convert.FromBase64String(payload));
                var root = doc.RootElement;

                if (root.TryGetProperty("exp", out var exp) &&
                    DateTimeOffset.FromUnixTimeSeconds(exp.GetInt64()) <= DateTimeOffset.UtcNow)
                    return null;

                var claims = new List<Claim>();
                foreach (var prop in root.EnumerateObject())
                {
                    var type = NormalizeType(prop.Name);
                    if (prop.Value.ValueKind == JsonValueKind.Array)
                    {
                        foreach (var item in prop.Value.EnumerateArray())
                            claims.Add(new Claim(type, item.ToString()));
                    }
                    else
                    {
                        claims.Add(new Claim(type, prop.Value.ToString()));
                    }
                }
                return claims;
            }
            catch (Exception ex) when (
                ex is FormatException or JsonException or InvalidOperationException)
            {
                return null;
            }
        }

        private static string NormalizeType(string type) => type switch
        {
            "name" or "unique_name" => ClaimTypes.Name,
            "role" or "roles" => ClaimTypes.Role,
            "nameid" => ClaimTypes.NameIdentifier,
            _ => type
        };
    }
}