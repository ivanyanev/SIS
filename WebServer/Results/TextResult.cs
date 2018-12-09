using HTTP.Enums;
using HTTP.Headers;
using HTTP.Responses;
using System.Text;

namespace WebServer.Results
{
    public class TextResult : HttpResponse
    {
        public TextResult(string content, HttpResponseStatusCode responseStatusCode)
            : base(responseStatusCode)
        {
            this.Headers.Add(new HttpHeader("Content-type", "text/plain; charset=utf-8"));
            this.Content = Encoding.UTF8.GetBytes(content);
        }
    }
}
