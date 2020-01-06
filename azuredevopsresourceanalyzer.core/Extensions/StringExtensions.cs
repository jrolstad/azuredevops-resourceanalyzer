using System.Globalization;

namespace azuredevopsresourceanalyzer.core.Extensions
{
    public static class StringExtensions
    {
        public static bool ContainsValue(this string value, string toFind, CultureInfo culture=null)
        {
            if (string.IsNullOrWhiteSpace(value))
                return false;
            if (string.IsNullOrWhiteSpace(toFind))
                return true;

            var resolvedCulture = culture ?? CultureInfo.CurrentCulture;
            return resolvedCulture.CompareInfo.IndexOf(value, toFind, CompareOptions.IgnoreCase) >= 0;
        }
    }
}