namespace Taxer.Core.Common;

public sealed class Error(string code, string message = default!)
{
    public string Code { get; } = code;
    public string Message { get; } = message;

    public static readonly Error None = new(string.Empty);

    public static implicit operator Result(Error error) => Result.Failure(error);
}
