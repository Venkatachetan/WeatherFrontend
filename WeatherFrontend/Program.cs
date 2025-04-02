using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using MudBlazor;
using MudBlazor.Services;
using WeatherFrontend;
using WeatherFrontend.Services;
using Weatherfrontend;
using Blazored.LocalStorage;


var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");
builder.Services.AddMudServices();
builder.Services.AddBlazoredLocalStorage();
builder.Services.AddScoped<UserHistoryService>();
builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri("https://weatherbackend-j6hf.onrender.com/") });

builder.Services.AddScoped<FavoriteCityService>();
// Add Authentication services
builder.Services.AddScoped<AuthService>();
builder.Services.AddScoped<AuthenticationStateProvider, CustomAuthStateProvider>();
builder.Services.AddAuthorizationCore();
builder.Services.AddMudServices();

await builder.Build().RunAsync();