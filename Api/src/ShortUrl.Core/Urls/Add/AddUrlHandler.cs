namespace ShortUrl.Core.Urls.Add;
public interface IUrlDataStore
{
  Task AddAsync(ShortenedUrl shortenedUrl, CancellationToken cancellationToken);
}
public class AddUrlHandler
{
  private readonly ShortUrlGenerator _shortUrlGenerator;
  private readonly IUrlDataStore _urlDataStore;

  public AddUrlHandler(ShortUrlGenerator shortUrlGenerator, IUrlDataStore urlDataStore)
  {
    _shortUrlGenerator = shortUrlGenerator;
    _urlDataStore = urlDataStore;
  }

  public async Task<AddUrlResponse> HandleAsync(AddUrlRequest request, CancellationToken cancellationToken)
  {
    var shortenedUrl = new ShortenedUrl(request.Url, _shortUrlGenerator.GenerateUniqueUrl(), default, default);

    await _urlDataStore.AddAsync(shortenedUrl, cancellationToken);

    return new AddUrlResponse(request.Url, shortenedUrl.ShortUrl);
  }
}