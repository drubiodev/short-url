using System;

namespace ShortUrl.Core.Urls.Add;

public interface IUrlDataStore
{
  Task AddAsync(ShortenedUrl shortenedUrl, CancellationToken cancellationToken);
}