namespace ShortUrl.Core;

public record Error(string Code, string Description)
{
  // default implementation
  public static Error None => new Error(string.Empty, string.Empty);
}
