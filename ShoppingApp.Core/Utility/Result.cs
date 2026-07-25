using System.Text.Json.Serialization;

namespace ShoppingApp.Core.Utility;

public class Result
{
    protected Result(bool isSuccess, Error? error)
    {
        if (isSuccess && error != null
            || !isSuccess && error == null)
        {
            throw new ArgumentException("Invalid error");
        }

        IsSuccess = isSuccess;
        Error = error;
    }

    public bool IsSuccess { get; }
    [JsonIgnore]
    public bool IsFailure => !IsSuccess;
    public Error? Error { get; }

    public static Result Success() => new(true, null);
    public static Result Failure(Error error) => new(false, error);
    public static Result<TValue> Success<TValue>(TValue value) => new(value);
    public static Result<TValue> Failure<TValue>(Error error) => new(error);
}

public class Result<T> : Result
{
    protected internal Result(T? value)
        : base(true, null)
    {
        Value = value;
    }

    protected internal Result(Error error)
        : base(false, error)
    {
        Value = default;
    }

    public T? Value { get; }
}
