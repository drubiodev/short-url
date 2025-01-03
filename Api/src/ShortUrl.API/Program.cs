using Azure.Identity;
using short_url.Extensions;
using ShortUrl.Core.Urls.Add;

var builder = WebApplication.CreateBuilder(args);

var keyVaultName = builder.Configuration["KeyVaultName"];

if (!string.IsNullOrEmpty(keyVaultName))
{
    builder.Configuration.AddAzureKeyVault(
        new Uri($"https://{keyVaultName}.vault.azure.net/"),
        new DefaultAzureCredential());
}

// Add services to the container.
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

// Services
builder.Services.AddSingleton(TimeProvider.System); // will use the system time provider
builder.Services.AddUrlFeatures();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();

app.MapGet("/", () => "Hello World!");

app.MapPost("/api/urls", async (AddUrlRequest request, AddUrlHandler handler, CancellationToken cancellationToken) =>
{
    var requestWithUser = request with { CreatedBy = "system" };
    var result = await handler.HandleAsync(request, cancellationToken);

    if (!result.Succeeded)
    {
        return Results.BadRequest(result.Error);
    }

    return Results.Created($"/api/urls/{result.Value!.ShortUrl}", result.Value);
});

app.Run();
