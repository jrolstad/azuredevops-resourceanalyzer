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
    }
}