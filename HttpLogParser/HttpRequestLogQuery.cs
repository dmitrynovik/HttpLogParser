using System;
using System.Collections.Generic;
using System.Linq;

namespace HttpLogParser
{
    public class HttpRequestLogQuery
    {
        public HttpRequestLogStatistics GetStatistics(IEnumerable<HttpRequestLog> logs, int top = 3)
        {
            if (logs is null)
                return new HttpRequestLogStatistics();

            return new HttpRequestLogStatistics(
                logs.Select(l => l.IpAddress).Distinct().Count(),
                GetTopUsed(logs, l => l.IpAddress, top),
                GetTopUsed(logs, l => l.Url, top)
            );
        }

        private IEnumerable<TProperty> GetTopUsed<TProperty>(IEnumerable<HttpRequestLog> logs, Func<HttpRequestLog, TProperty> propertySelector, int count)
        {
            if (logs is null || count == 0)
                return Enumerable.Empty<TProperty>();

            if (propertySelector is null)
                throw new ArgumentNullException(nameof(propertySelector));

            var groupings = new Dictionary<TProperty, int>();

            logs
                .Select(l => propertySelector(l))
                .ToList()
                .ForEach(p =>
                {
                    var count = groupings.TryGetValue(p, out var pValue) ? pValue : 0;
                    groupings[p] = count + 1;
                });

            return groupings
                .OrderByDescending(kvp => kvp.Value)
                .ThenBy(kvp => kvp.Key)
                .Select(kvp => kvp.Key)
                .Take(count);
        }
    }
}
