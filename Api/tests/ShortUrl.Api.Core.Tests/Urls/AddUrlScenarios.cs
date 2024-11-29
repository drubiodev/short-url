using System.Net;
using FluentAssertions;
using ShortUrl.Api.Core.Tests.TestDoubles;
using ShortUrl.Core;
using ShortUrl.Core.Urls.Add;

namespace ShortUrl.Api.Core.Tests.Urls;

public class AddUrlScenarios
{
  private readonly AddUrlHandler _handler;
  private readonly InMemoryUrlDataStore _urlDataStore = new();

  public AddUrlScenarios()
  {
    var tokenProvider = new TokenProvider();
    tokenProvider.AssignRange(1, 5);
    var shortUrlGenerator = new ShortUrlGenerator(tokenProvider);
    _handler = new AddUrlHandler(shortUrlGenerator, _urlDataStore);
  }

  [Fact]
  public async Task Should_return_short_url()
  {
    var request = CreateAddUrlRequest();

    var response = await _handler.HandleAsync(request, default);

    response.ShortUrl.Should().NotBeNullOrEmpty();
    response.ShortUrl.Should().Be("1");
  }

  [Fact]
  public async Task Should_save_short_url()
  {
    var request = CreateAddUrlRequest();

    var response = await _handler.HandleAsync(request, default);

    _urlDataStore.Should().ContainKey(response.ShortUrl);
  }

  private static AddUrlRequest CreateAddUrlRequest() =>
    new AddUrlRequest(new Uri("https://www.microsoft.com"));
}