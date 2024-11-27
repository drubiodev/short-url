using ShortUrl.Core.Extensions;

namespace ShortUrl.Core;

public class ShortUrlGenerator
{
  private readonly TokenProvider _tokenProvider;

  public ShortUrlGenerator(TokenProvider tokenProvider)
  {
    _tokenProvider = tokenProvider;
  }
  public string GenerateUniqueUrl()
  {
    return _tokenProvider
        .GetToken()
        .ToBase62();
  }
}