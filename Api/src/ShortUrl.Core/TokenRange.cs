namespace ShortUrl.Core;

public record TokenRange
{
  public TokenRange(int start, int end)
  {
    if (end < start)
      throw new ArgumentException("End must be greater than or equal to start");

    Start = start;
    End = end;
  }

  public int Start { get; }
  public int End { get; }
}