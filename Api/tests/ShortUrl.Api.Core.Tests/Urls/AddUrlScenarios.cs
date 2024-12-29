using System.Net;
using FluentAssertions;
using Microsoft.Extensions.Time.Testing;
using ShortUrl.Api.Core.Tests.TestDoubles;
using ShortUrl.Core;
using ShortUrl.Core.Urls.Add;

namespace ShortUrl.Api.Core.Tests.Urls;

public class AddUrlScenarios
{
  private readonly AddUrlHandler _handler;
  private readonly InMemoryUrlDataStore _urlDataStore = new();
  private readonly FakeTimeProvider _timeProvider;

  public AddUrlScenarios()
  {
    var tokenProvider = new TokenProvider();
    tokenProvider.AssignRange(1, 5);
    var shortUrlGenerator = new ShortUrlGenerator(tokenProvider);
    _timeProvider = new FakeTimeProvider();
    _handler = new AddUrlHandler(shortUrlGenerator, _urlDataStore, _timeProvider);
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

  [Fact]
  public async Task Should_save_short_url_with_created_by_and_created_on()
  {
    var request = CreateAddUrlRequest();

    var response = await _handler.HandleAsync(request, default);

    _urlDataStore.Should().ContainKey(response.ShortUrl);
    _urlDataStore[response.ShortUrl].CreatedBy.Should().Be(request.CreatedBy);
    _urlDataStore[response.ShortUrl].CreatedOn.Should().Be(_timeProvider.GetUtcNow());
  }

  [Fact]
  public async Task Should_return_error_if_created_by_is_empty()
  {
    var request = CreateAddUrlRequest(string.Empty);
    var response = await _handler.HandleAsync(request, default);
  }

  private static AddUrlRequest CreateAddUrlRequest(string createdBy = "user@user.com") =>
    new AddUrlRequest(new Uri("https://www.microsoft.com"), createdBy);
}