using DotUrlShortener.ServiceDefaults;
using DotUrlShortener.Ui.Clients;
using DotUrlShortener.Ui.Clients.Interfaces;
using DotUrlShortener.Ui.Components;

var builder = WebApplication.CreateBuilder(args);

builder.AddServiceDefaults();

builder.Services.AddHttpClient<IDotUrlShortenerClient, DotUrlShortenerClient>(e =>
    e.BaseAddress = new Uri(builder.Configuration["DotUrlShortener.ApiEndpointHttps"]!)
);

// Add services to the container.
builder.Services.AddRazorComponents();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();

app.UseAntiforgery();

app.MapStaticAssets();
app.MapRazorComponents<App>();

app.Run();
