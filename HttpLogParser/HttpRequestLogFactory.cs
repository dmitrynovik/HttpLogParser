using Functional.Maybe;

namespace HttpLogParser
{
    public class HttpRequestLogFactory
    {
        public Maybe<HttpRequestLog> Parse(string line)
        {
            if (string.IsNullOrWhiteSpace(line))
                return Maybe<HttpRequestLog>.Nothing;


            var tokens = line.Split(' ', System.StringSplitOptions.RemoveEmptyEntries);
            if (tokens.Length < 7)
                return Maybe<HttpRequestLog>.Nothing;


            var ip = tokens[0];
            var url = tokens[6];
            return new HttpRequestLog(ip, url).ToMaybe();
        }
    }
}
