namespace ShortUrl.Core;

public class TokenProvider
{
  private readonly object _tokenLock = new();

  private int _token = 0;
  private TokenRange? _tokenRange;
  public void AssignRange(int start, int end)
  {
    AssignRange(new TokenRange(start, end));
  }

  public void AssignRange(TokenRange tokenRange)
  {
    _tokenRange = tokenRange;
    _token = tokenRange.Start;
  }

  public int GetToken()
  {
    lock (_tokenLock)
    {
      return _token++;
    }
  }
}