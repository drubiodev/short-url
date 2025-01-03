using ShortUrl.Core;
using ShortUrl.Core.Urls;
using ShortUrl.Core.Urls.Add;

namespace short_url.Extensions;

public static class ServiceCollectionExtensions
{
  public static IServiceCollection AddUrlFeatures(this IServiceCollection services)
  {
    services.AddScoped<AddUrlHandler>();
    services.AddSingleton(_ =>
    {
      var tokenProvider = new TokenProvider();
      tokenProvider.AssignRange(1, 1000);
      return tokenProvider;
    });
    services.AddScoped<ShortUrlGenerator>();
    services.AddSingleton<IUrlDataStore, InMemoryUrlDataStore>();

    return services;
  }

  private class InMemoryUrlDataStore : Dictionary<string, ShortenedUrl>, IUrlDataStore
  {
    public Task AddAsync(ShortenedUrl shortenedUrl, CancellationToken cancellationToken)
    {
      Add(shortenedUrl.ShortUrl, shortenedUrl);

      return Task.CompletedTask;
    }
  }

}
