using System.Collections.Generic;
using System.Text;

public static class IEnumerableExtensions
{
    public static string AsString(this IEnumerable<int> source)
    {
        var sb = new StringBuilder();
        sb.Append("<IEnumerable>{");
        bool first = true;
        foreach (var item in source)
        {
            if (!first)
                sb.Append(", ");
            sb.Append(item);
            first = false;
        }
        sb.Append("}");
        return sb.ToString();
    }
}
