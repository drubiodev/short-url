using ShortUrl.Core.Urls;
using ShortUrl.Core.Urls.Add;
using Microsoft.Azure.Cosmos;
using System.Text.Json.Serialization;
using Newtonsoft.Json;

namespace ShortUrl.Infrastructure;

public class CosmosDbUrlDataStore : IUrlDataStore
{
  private readonly Container _container;

  public CosmosDbUrlDataStore(Container container)
  {
    _container = container;
  }

  public async Task AddAsync(ShortenedUrl shortenedUrl, CancellationToken cancellationToken)
  {
    var document = (ShortenedUrlCosmos)shortenedUrl;
    await _container.CreateItemAsync(document, new PartitionKey(document.PartitionKey), cancellationToken: cancellationToken);
  }
}

internal class ShortenedUrlCosmos
{
  public string LongUrl { get; }
  [JsonProperty(PropertyName = "id")] // Cosmos DB Unique Identifier
  public string ShortUrl { get; }
  public DateTimeOffset CreatedOn { get; }
  public string CreatedBy { get; }
  public string PartitionKey => ShortUrl[..1];

  public ShortenedUrlCosmos(string longUrl, string shortUrl, DateTimeOffset createdOn, string createdBy)
  {
    LongUrl = longUrl;
    ShortUrl = shortUrl;
    CreatedOn = createdOn;
    CreatedBy = createdBy;
  }

  public static implicit operator ShortenedUrlCosmos(ShortenedUrl shortenedUrl)
  {
    return new ShortenedUrlCosmos(shortenedUrl.LongUrl.ToString(), shortenedUrl.ShortUrl, shortenedUrl.CreatedOn, shortenedUrl.CreatedBy);
  }

  public static implicit operator ShortenedUrl(ShortenedUrlCosmos shortenedUrlCosmos)
  {
    return new ShortenedUrl(new Uri(shortenedUrlCosmos.LongUrl), shortenedUrlCosmos.ShortUrl, shortenedUrlCosmos.CreatedBy, shortenedUrlCosmos.CreatedOn);
  }
}
