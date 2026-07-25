namespace ShoppingApp.Core.Utility;

public sealed record Error(string Code, 
    string Message)
{
    public static readonly Error None = new("", "");

    public static implicit operator Result(Error error) => Result.Failure(error);
}
