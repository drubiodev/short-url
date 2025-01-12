using Azure.Identity;
using ShortUrl.Extensions;
using ShortUrl.Core.Urls.Add;
using ShortUrl.Infrastructure.Extensions;

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
builder.Services
    .AddUrlFeatures()
    .AddCosmosUrlDataStore(builder.Configuration);

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();

app.MapGet("/", () => "ShortUrl API")
    .WithName("ShortUrlApi");

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
