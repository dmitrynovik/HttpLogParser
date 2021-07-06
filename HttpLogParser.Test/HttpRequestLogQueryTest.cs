using FluentAssertions;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Xunit;

namespace HttpLogParser.Test
{
    public class HttpRequestLogQueryTest
    {
        [Fact]
        public void In_Example_File_Are_11_Unique_IPs()
        {
            var logs = ParseLogs();
            var statistics = new HttpRequestLogQuery().GetStatistics(logs.Select(l => l.Value));
            statistics.UniqueIPs.Should().Be(11);
        }

        [Fact]
        public void In_Example_File_Top_IPs_Are_As_Expected()
        {
            var logs = ParseLogs();
            var statistics = new HttpRequestLogQuery().GetStatistics(logs.Select(l => l.Value));
            statistics.TopUsedIPs.Should().BeEquivalentTo(new[] { "168.41.191.40", "177.71.128.21", "50.112.00.11" });
        }

        [Fact]
        public void In_Example_File_Top_Urls_Are_As_Expected()
        {
            var logs = ParseLogs();
            var statistics = new HttpRequestLogQuery().GetStatistics(logs.Select(l => l.Value));
            statistics.TopUsedUrls.Should().BeEquivalentTo(new[] { "/docs/manage-websites/", "/", "/asset.css" });
        }

        [Fact]
        public void In_Example_File_All_Lines_Can_Be_Parsed()
        {
            var logs = ParseLogs();
            logs.Select(l => l.HasValue ? l.Value : null).Should().NotContainNulls();
        }

        private static IEnumerable<Functional.Maybe.Maybe<HttpRequestLog>> ParseLogs()
        {
            var text = File.ReadAllText("Data/example.txt");
            var factory = new HttpRequestLogFactory();

            return text
                .Split("\n", System.StringSplitOptions.RemoveEmptyEntries)
                .Select(l => factory.Parse(l));
        }
    }
}
