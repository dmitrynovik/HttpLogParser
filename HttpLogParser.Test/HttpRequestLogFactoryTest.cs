using FluentAssertions;
using FluentAssertions.Execution;
using Xunit;

namespace HttpLogParser.Test
{
    public class HttpRequestLogFactoryTest
    {
        [Fact]
        public void When_Log_Line_Is_Malformed_The_HttpLogRequest_Parse_Returns_Nothing()
        {
            const string line = "177.71.128.21 - - [10/Jul/2018:22:21:28 +0200] \"GET";
            var log = new HttpRequestLogFactory().Parse(line);
            log.HasValue.Should().BeFalse();
        }

        [Fact]
        public void When_Log_Line_Is_Good_The_HttpLogRequest_Parses_As_Expected()
        {
            const string line = "177.71.128.21 - - [10/Jul/2018:22:21:28 +0200] \"GET /intranet-analytics/ HTTP/1.1\" 200 3574 \"-\" \"Mozilla / 5.0(X11; U; Linux x86_64; fr - FR) AppleWebKit / 534.7(KHTML, like Gecko) Epiphany / 2.30.6 Safari / 534.7\"";
            var log = new HttpRequestLogFactory().Parse(line);
            log.HasValue.Should().BeTrue();

            using (new AssertionScope())
            {
                log.Value.IpAddress.Should().Be("177.71.128.21");
                log.Value.Url.Should().Be("/intranet-analytics/");
            }
        }

    }
}
