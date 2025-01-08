using ReduceUrl.ServiceDefaults;
using ReduceUrl.Ui.Clients;
using ReduceUrl.Ui.Clients.Interfaces;
using ReduceUrl.Ui.Components;

var builder = WebApplication.CreateBuilder(args);

builder.AddServiceDefaults();

builder.Services.AddHttpClient<IReduceUrlClient, ReduceUrlClient>(e =>
    e.BaseAddress = new Uri(builder.Configuration["HttpResources:ReduceUrlApiEndpointHttps"]!)
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