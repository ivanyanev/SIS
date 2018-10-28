using HTTP.Enums;
using HTTP.Requests;
using HTTP.Responses;
using WebServer.Results;

namespace CakesWebApp.Controlers
{
    public class HomeController : BaseControler
    {
        public IHttpResponse Index(IHttpRequest request)
        {
            return this.View("Index");
        }

        public IHttpResponse HelloUser(IHttpRequest request)
        {
            return new HtmlResult($"<h1>Hello, {this.GetUsername(request)}</h1>", HttpResponseStatusCode.Ok);
        }
    }
}