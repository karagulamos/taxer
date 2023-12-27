using Taxer.Core.Common;

namespace Taxer.Core.Errors;

public static class ServiceErrors
{
    public sealed class Tax
    {
        public static readonly Error InvalidPostalCode = Create<Tax>(nameof(InvalidPostalCode), "Invalid postal code.");
        public static readonly Error InvalidIncome = Create<Tax>(nameof(InvalidIncome), "Invalid income.");
        public static readonly Error UnsupportedPostalCode = Create<Tax>(nameof(UnsupportedPostalCode), "Unsupported postal code.");
    }

    private static string Code<T>(string name) => $"{typeof(T).Name}.{name}";
    private static Error Create<TClass>(string error, string message) => new(Code<TClass>(error), message);
}