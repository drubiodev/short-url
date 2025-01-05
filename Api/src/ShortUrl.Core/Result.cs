using System;

namespace ShortUrl.Core;

public class Result<TValue>
{
  private readonly TValue? _value;
  private readonly Error _error;
  private readonly bool _isSuccess;

  private Result(TValue? value)
  {
    _value = value;
    _error = Error.None;
    _isSuccess = true;
  }

  private Result(Error error)
  {
    _value = default;
    _error = error;
    _isSuccess = false;
  }

  public bool Succeeded => _isSuccess;
  public TValue? Value => _value;
  public Error Error => _error;

  public static Result<TValue> Success(TValue value) => new Result<TValue>(value);
  public static Result<TValue> Failure(Error error) => new Result<TValue>(error);

  public static implicit operator Result<TValue>(TValue value) => new(value);
  public static implicit operator Result<TValue>(Error error) => new(error);

  public TResult Match<TResult>(Func<TValue, TResult> success, Func<Error, TResult> failure) => _isSuccess ? success(_value!) : failure(_error);
}
