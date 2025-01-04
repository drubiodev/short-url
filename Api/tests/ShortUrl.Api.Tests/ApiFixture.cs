using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.DependencyInjection;
using ShortUrl.Core.Urls;
using ShortUrl.Core.Urls.Add;
using ShortUrl.Api.Tests.Extensions;

namespace ShortUrl.Api.Tests;

// WebApplicationFactory creates an in-memory test server for integration testing
public class ApiFixture : WebApplicationFactory<IApiAssemblyMarker>
{
  override protected void ConfigureWebHost(IWebHostBuilder builder)
  {
    builder.ConfigureServices(services =>
    {
      services.Remove<IUrlDataStore>();
      services.AddSingleton<IUrlDataStore>(new InMemoryUrlDataStore());
    });
  }
}

public class InMemoryUrlDataStore : Dictionary<string, ShortenedUrl>, IUrlDataStore
{
  public Task AddAsync(ShortenedUrl shortenedUrl, CancellationToken cancellationToken)
  {
    Add(shortenedUrl.ShortUrl, shortenedUrl);

    return Task.CompletedTask;
  }
}

