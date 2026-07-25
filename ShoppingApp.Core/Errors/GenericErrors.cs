using ShoppingApp.Core.Utility;

namespace ShoppingApp.Core.Errors;

public static class GenericErrors
{
    public static Error InvalidParam(string paramName) => new("Generic.InvalidParameter", $"Parameter {paramName} has invalid value.");
}
