using System;

namespace azuredevopsresourceanalyzer.ui.blazor.tests.TestUtility.Extensions
{
    public static class StringExtensions
    {
        public static DateTime? ToDateTime(this string value)
        {
            if (!string.IsNullOrWhiteSpace(value))
            {
                return DateTime.Parse(value);
            }
            else
            {
                return null;
            }
        }
        public static int ToInt32(this string value)
        {
            if (!string.IsNullOrWhiteSpace(value))
            {
                return int.Parse(value);
            }
            else
            {
                return 0;
            }
        }
    }
}