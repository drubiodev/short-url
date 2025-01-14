using FluentAssertions;
using ShortUrl.Core;
using ShortUrl.Core.Urls.Add;

namespace ShortUrl.Api.Core.Tests;

public class ShortUrlGeneratorScenarios
{
  [Fact]
  public void Should_return_ShortUrl_for_10001()
  {
    var tokenProvider = new TokenProvider();
    tokenProvider.AssignRange(10001, 20000);
    var shortUrlGenerator = new ShortUrlGenerator(tokenProvider);

    var shortUrl = shortUrlGenerator.GenerateUniqueUrl();

    shortUrl.Should().Be("2bJ");
  }

  [Fact]
  public void Should_return_ShortUrl_for_zero()
  {
    var tokenProvider = new TokenProvider();
    tokenProvider.AssignRange(0, 10);
    var shortUrlGenerator = new ShortUrlGenerator(tokenProvider);

    var shortUrl = shortUrlGenerator.GenerateUniqueUrl();

    shortUrl.Should().Be("0");
  }
}