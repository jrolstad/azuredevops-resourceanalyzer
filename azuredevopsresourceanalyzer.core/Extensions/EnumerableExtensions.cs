using System;
using System.Collections.Generic;
using System.Linq;

namespace azuredevopsresourceanalyzer.core.Extensions
{
    public static class EnumerableExtensions
    {
        public static IEnumerable<IEnumerable<T>> Segment<T>(this IEnumerable<T> enumerable, int segmentSize)
        {
            IEnumerable<IEnumerable<T>> segmented = null;

            if (segmentSize <= 0)
            {
                throw new ArgumentOutOfRangeException("segmentSize", segmentSize, "segmentSize must be larger than zero");
            }

            if (enumerable != null)
            {
                var enumerableArray = enumerable.ToArray();
                segmented = Enumerable.Range(0, enumerableArray.Length)
                    .GroupBy(i => i / segmentSize, i => enumerableArray[i])
                    .ToArray();
            }
            return segmented;
        }

        public static double? Median<TColl, TValue>(
            this IEnumerable<TColl> source,
            Func<TColl, TValue> selector)
        {
            return source.Select<TColl, TValue>(selector).Median();
        }

        public static double? Median<T>(
            this IEnumerable<T> source)
        {
            if (Nullable.GetUnderlyingType(typeof(T)) != null)
                source = source.Where(x => x != null);

            int count = source.Count();
            if (count == 0)
                return null;

            source = source.OrderBy(n => n);

            int midpoint = count / 2;
            if (count % 2 == 0)
                return (Convert.ToDouble(source.ElementAt(midpoint - 1)) + Convert.ToDouble(source.ElementAt(midpoint))) / 2.0;
            else
                return Convert.ToDouble(source.ElementAt(midpoint));
        }
    }
}