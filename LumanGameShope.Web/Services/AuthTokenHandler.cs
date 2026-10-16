using System.Net;
using System.Net.Http.Headers;
using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;

namespace LumanGameShope.Web.Services
{
    public sealed class AuthTokenHandler : DelegatingHandler
    {
        private readonly IJSRuntime _js;
        private readonly JwtAuthenticationStateProvider _auth;
        private readonly NavigationManager _nav;

        public AuthTokenHandler(
            IJSRuntime js,
            JwtAuthenticationStateProvider auth,
            NavigationManager nav)
        {
            _js = js;
            _auth = auth;
            _nav = nav;
        }

        protected override async Task<HttpResponseMessage> SendAsync(
            HttpRequestMessage request, CancellationToken cancellationToken)
        {
            var token = await _js.InvokeAsync<string?>(
                "sessionStorage.getItem", cancellationToken, "authToken");

            if (!string.IsNullOrWhiteSpace(token))
                request.Headers.Authorization =
                    new AuthenticationHeaderValue("Bearer", token);

            var response = await base.SendAsync(request, cancellationToken);

            // فقط اگه توکن داشتیم: ۴۰۱ لاگین اشتباه نباید کاربر رو ریدایرکت کنه
            if (response.StatusCode == HttpStatusCode.Unauthorized &&
                !string.IsNullOrWhiteSpace(token))
            {
                await _auth.SignOutAsync();
                var returnUrl = Uri.EscapeDataString(_nav.ToBaseRelativePath(_nav.Uri));
                _nav.NavigateTo($"login?returnUrl={returnUrl}");
            }

            return response;
        }
    }
}