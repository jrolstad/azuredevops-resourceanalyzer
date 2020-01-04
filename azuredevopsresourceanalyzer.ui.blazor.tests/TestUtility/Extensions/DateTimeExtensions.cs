using System;

namespace azuredevopsresourceanalyzer.ui.blazor.tests.TestUtility.Extensions
{
    public static class DateTimeExtensions
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
    }
}