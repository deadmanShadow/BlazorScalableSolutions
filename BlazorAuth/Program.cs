using BlazorAuth.AppSettings;
using BlazorAuth.Components;
using BlazorAuth.Models;
using BlazorAuth.Services;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components.Server.ProtectedBrowserStorage;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents();

// Create a list of users to inject into UserInfoService
var users = new List<UserInfo>();

builder.Services.AddSingleton(users); // Register the list as a singleton

builder.Services.AddAuthentication()
    .AddScheme<AuthenticationSchemeOptions, CustomAuthHandler>("CustomAuth", null);
builder.Services.AddCascadingAuthenticationState();
builder.Services.AddAuthenticationCore();
builder.Services.AddAuthorization();

builder.Services.AddServerSideBlazor();
builder.Services.AddScoped<ProtectedLocalStorage>();
builder.Services.AddScoped<AuthenticationStateProvider, CustomAuthStateProvider>();
builder.Services.AddTransient<UserInfoService>();
builder.Services.AddScoped<IWeatherForecastService, WeatherForecastService>();

var app = builder.Build();
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
    app.UseHsts();
}

app.UseHttpsRedirection();

app.UseStaticFiles();
app.UseAntiforgery();

app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode();

app.Run();
