using ShortUrl.Core.Urls;
using ShortUrl.Core.Urls.Add;

namespace ShortUrl.Api.Core.Tests.TestDoubles;

public class InMemoryUrlDataStore : Dictionary<string, ShortenedUrl>, IUrlDataStore
{
  public Task AddAsync(ShortenedUrl shortenedUrl, CancellationToken cancellationToken)
  {
    Add(shortenedUrl.ShortUrl, shortenedUrl);

    return Task.CompletedTask;
  }
}
