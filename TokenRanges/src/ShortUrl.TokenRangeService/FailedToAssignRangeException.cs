namespace ShortUrl.TokenRangeService;

public class FailedToAssignRangeException : Exception
{
  public FailedToAssignRangeException(string message) : base(message)
  {
  }
}