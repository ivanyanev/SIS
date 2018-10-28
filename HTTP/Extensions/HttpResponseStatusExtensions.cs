using HTTP.Enums;

namespace HTTP.Extensions
{
    public static class HttpResponseStatusExtensions
    {
        public static string GetResponseLine(this HttpResponseStatusCode statusCode) => $"{(int)statusCode} {statusCode}";
    }
}