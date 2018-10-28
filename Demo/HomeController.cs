using HTTP.Enums;
using HTTP.Responses;
using WebServer.Results;

namespace Demo
{
    public class HomeController
    {
        public IHttpResponse Index()
        {
            string content = "<h1>Hello, World!</h1>";

            return new HtmlResult(content, HttpResponseStatusCode.Ok);
        }
    }
}