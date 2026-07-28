using ShoppingApp.Core.Utility;

namespace ShoppingApp.Core.Errors;

public static class GenericErrors
{
    public static Error Generic() => new("Generic.Generic", "Generic error occured.");
    public static Error InvalidParam(string paramName) => new("Generic.InvalidParameter", $"Parameter {paramName} has invalid value.");
}
