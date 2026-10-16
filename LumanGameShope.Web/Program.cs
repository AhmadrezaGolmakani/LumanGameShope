using LumanGameShope.Web;
using LumanGameShope.Web.Services;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using MudBlazor.Services;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");
var apiBaseUrl = builder.Configuration["ApiBaseUrl"];

builder.Services.AddScoped(sp => new HttpClient
{
    BaseAddress = new Uri(apiBaseUrl)
});
builder.Services.AddMudServices();


builder.Services.AddAuthorizationCore();

// Singleton چون داخل handlerهای HttpClientFactory باید همون نمونه UI باشه
builder.Services.AddSingleton<JwtAuthenticationStateProvider>();
builder.Services.AddSingleton<AuthenticationStateProvider>(
    sp => sp.GetRequiredService<JwtAuthenticationStateProvider>());

builder.Services.AddTransient<AuthTokenHandler>();

builder.Services.AddHttpClient("Api", client =>
    client.BaseAddress = new Uri("https://localhost:7103"))   // همون آدرس قبلی
    .AddHttpMessageHandler<AuthTokenHandler>();

// هر سرویسی که HttpClient بگیره (AuthService و بقیه) خودکار handler رو داره
builder.Services.AddScoped(sp =>
    sp.GetRequiredService<IHttpClientFactory>().CreateClient("Api"));

builder.Services.AddScoped<AuthService>();

await builder.Build().RunAsync();
