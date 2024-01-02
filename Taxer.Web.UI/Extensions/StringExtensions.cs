using System.Text;

namespace Taxer.Web.UI.Extensions;

public static class StringExtensions
{
    public static string ToSeparatedWords(this string value)
    {
        if (string.IsNullOrWhiteSpace(value))
            return string.Empty;

        var sb = new StringBuilder(value[0].ToString());

        foreach (var c in value[1..])
        {
            if (char.IsUpper(c))
                sb.Append(' ');

            sb.Append(c);
        }

        return sb.ToString();
    }
}