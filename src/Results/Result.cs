using System;

namespace TropicalCode.FunctionalHelpers.Results;

public sealed class Result<T>
{
    public Error Error { get; }
    public T Value { get; }
    public bool IsSuccess { get; }

    private Result(T value)
    {
        Value = value;
        IsSuccess = true;
        Error = Error.Empty;
    }
    
    private Result(Error error)
    {
        Error = error ?? throw new ArgumentNullException(nameof(error));
        IsSuccess = false;
        Value = default!;
    }
    
    public static Result<T> Success(T value) => new(value);
    public static Result<T> Failed(Error error) => new(error);
}