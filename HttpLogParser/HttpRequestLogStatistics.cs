using System.Collections.Generic;
using System.Linq;

namespace HttpLogParser
{
    public class HttpRequestLogStatistics
    {
        public HttpRequestLogStatistics() { }
        public HttpRequestLogStatistics(int uniqueIPs, IEnumerable<string> topUsedIPs, IEnumerable<string> topUsedUrls)
        {
            UniqueIPs = uniqueIPs;
            TopUsedIPs = topUsedIPs?.ToArray() ?? new string[0];
            TopUsedUrls = topUsedUrls?.ToArray() ?? new string[0];
        }

        public int UniqueIPs { get; internal set; }
        public string[] TopUsedIPs { get; internal set; } = new string[0];
        public string[] TopUsedUrls { get; internal set; } = new string[0];
    }
}
