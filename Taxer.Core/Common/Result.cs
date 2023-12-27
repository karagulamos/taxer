namespace Taxer.Core.Common;

public abstract class ResultBase
{
    public ResultBase(bool isSuccess, Error error)
    {
        if (isSuccess && error != Error.None)
            throw new ArgumentException("Invalid result state");

        if (!isSuccess && error == Error.None)
            throw new ArgumentException("Invalid result state");

        IsSuccess = isSuccess;
        Error = error;
    }

    public bool IsSuccess { get; protected set; }
    public Error Error { get; protected set; }
}

public sealed class Result(bool isSuccess, Error error) : ResultBase(isSuccess, error)
{
    public static readonly Result Success = new(true, Error.None);
    public static Result Failure(Error error) => new(false, error);
}

public sealed class Result<T>(bool isSuccess, Error error, T value) : ResultBase(isSuccess, error)
{
    public T Value { get; } = value;

    public static Result<T> Success(T value) => new(true, Error.None, value);
    public static Result<T> Failure(Error error) => new(false, error, default!);

    public static implicit operator Result(Result<T> result) => result.IsSuccess ? Success(result.Value) : Failure(result.Error);

    public static implicit operator Result<T>(T value) => Success(value);

    public static implicit operator Result<T>(Error error) => Failure(error);
}