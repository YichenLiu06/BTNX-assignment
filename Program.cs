using BTNX_assignment.Components;
using Microsoft.EntityFrameworkCore;
using ProfileAPI;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents();

builder.Services.AddLogging();

builder.Services.AddDbContext<ProfileDb>(opt => opt.UseInMemoryDatabase("Profiles"));
builder.Services.AddHttpClient("ProfileAPI", client => {
    client.BaseAddress = new Uri("http://localhost:5174/api/");
});
builder.Logging.ClearProviders();  // Optional, clears default logging providers
builder.Logging.AddConsole();      // Add console logging
builder.Logging.AddDebug();        // Add debug logging

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddOpenApiDocument(config =>
{
    config.DocumentName = "ProfileAPI";
    config.Title = "ProfileAPI v1";
    config.Version = "v1";
});

var app = builder.Build();

ProfileEndpoints.MapProfileEndpoints(app.MapGroup("/api/profiles"));

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

if (app.Environment.IsDevelopment())
{
    app.UseOpenApi();
    app.UseSwaggerUi(config =>
    {
        config.DocumentTitle = "ProfileAPI";
        config.Path = "/swagger";
        config.DocumentPath = "/swagger/{documentName}/swagger.json";
        config.DocExpansion = "list";
    });
}

app.UseStaticFiles();
app.UseAntiforgery();

app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode();

app.Run();
