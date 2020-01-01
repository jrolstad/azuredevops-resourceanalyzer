using System.Globalization;

namespace azuredevopsresourceanalyzer.core.Extensions
{
    public static class StringExtensions
    {
        public static bool ContainsValue(this string value, string toFind, CultureInfo culture)
        {
            return culture.CompareInfo.IndexOf(value, toFind, CompareOptions.IgnoreCase) >= 0;
        }
    }
}