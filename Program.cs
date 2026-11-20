using System;
using System.Net.Http;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using MidtermAppDev;
using MidtermAppDev.Services;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });
builder.Services.AddScoped<GameSessionState>();

// Step 4: Supabase Client Configuration & Singleton Service Registration
var url = builder.Configuration["Supabase:Url"] ?? "https://YOUR-REF.supabase.co";
var key = builder.Configuration["Supabase:Key"] ?? "sb_publishable_placeholder";
var supabase = new Supabase.Client(url, key, new Supabase.SupabaseOptions { AutoConnectRealtime = false });

try
{
    if (!string.IsNullOrWhiteSpace(url) && !url.Contains("YOUR-REF"))
    {
        await supabase.InitializeAsync();
    }
}
catch (Exception ex)
{
    Console.WriteLine($"[Supabase] Initialization deferred: {ex.Message}");
}

builder.Services.AddSingleton(supabase);

await builder.Build().RunAsync();
