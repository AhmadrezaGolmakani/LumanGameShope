using LumanGameShope.Web.DTOs.User;
using Microsoft.JSInterop;
using System.Net.Http.Json;

namespace LumanGameShope.Web.Services
{
    public sealed class AuthService
    {
        private readonly HttpClient _httpClient;
        private readonly IJSRuntime _js;

        public AuthService(HttpClient httpClient, IJSRuntime js)
        {
            _httpClient = httpClient;
            _js = js;
        }

        public async Task<LoginResponse?> LoginAsync(
            LoginRequest request,
            CancellationToken cancellationToken = default)
        {
            using var response = await _httpClient.PostAsJsonAsync(
                "api/v1/Account/Login",
                request,
                cancellationToken);

            if (!response.IsSuccessStatusCode)
                return null;

            var result = await response.Content
                .ReadFromJsonAsync<LoginResponse>(
                    cancellationToken: cancellationToken);

            if (result is null ||
                string.IsNullOrWhiteSpace(result.JwtToken))
            {
                return null;
            }

            await _js.InvokeVoidAsync(
                "sessionStorage.setItem",
                cancellationToken,
                "authToken",
                result.JwtToken);

            await _js.InvokeVoidAsync(
                "sessionStorage.setItem",
                cancellationToken,
                "userName",
                result.UserName);

            await _js.InvokeVoidAsync(
                "sessionStorage.setItem",
                cancellationToken,
                "userId",
                result.UserId.ToString());


            UserName = result.UserName;
            IsLoggedIn = true;

            AuthStateChanged?.Invoke();

            return result;
        }

        public async Task LogoutAsync()
        {
            await _js.InvokeVoidAsync(
                "sessionStorage.removeItem", "authToken");

            await _js.InvokeVoidAsync(
                "sessionStorage.removeItem", "userName");

            await _js.InvokeVoidAsync(
                "sessionStorage.removeItem", "userId");

            UserName = null;
            IsLoggedIn = false;

            AuthStateChanged?.Invoke();
        }



        public event Action? AuthStateChanged;

        public bool IsLoggedIn { get; private set; }

        public string? UserName { get; private set; }

        public async Task InitializeAsync()
        {
            var token = await _js.InvokeAsync<string?>(
                "sessionStorage.getItem", "authToken");

            UserName = await _js.InvokeAsync<string?>(
                "sessionStorage.getItem", "userName");

            IsLoggedIn = !string.IsNullOrWhiteSpace(token);

            AuthStateChanged?.Invoke();
        }
    }
}
