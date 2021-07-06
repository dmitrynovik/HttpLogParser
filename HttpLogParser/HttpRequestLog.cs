namespace HttpLogParser
{
    public class HttpRequestLog
    {
        public HttpRequestLog(string ipAddress, string url)
        {
            IpAddress = ipAddress;
            Url = url;
        }

        public string IpAddress { get; }
        public string Url { get; }
    }
}
