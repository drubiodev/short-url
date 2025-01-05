namespace ShortUrl.Core.Extensions;

public static class Base62Extensions
{
  const string base62 = "0123456789ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz";
  public static string ToBase62(this long number)
  {
    if (number == 0) return "0";
    Stack<char> stack = new Stack<char>();

    while (number > 0)
    {
      stack.Push(base62[(int)number % 62]);
      number /= 62;
    }

    return new string(stack.ToArray());
  }
}