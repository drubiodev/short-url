namespace ShortUrl.Core;

public static class Errors
{
  public static Error MissingCreatedBy => new Error("missing_value", "CreatedBy is required");
}
