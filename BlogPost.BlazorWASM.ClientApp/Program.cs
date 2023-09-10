using BlogPost.BlazorWASM.ClientApp;
using BlogPost.BlazorWASM.ClientApp.Services.Base;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri("https://localhost:7030/") });
builder.Services.AddScoped<IClient, Client>();

await builder.Build().RunAsync();
