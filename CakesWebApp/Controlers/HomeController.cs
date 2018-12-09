using HTTP.Enums;
using HTTP.Requests;
using HTTP.Responses;
using System.IO;
using System.Text;
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
            var sb = new StringBuilder();
            sb.Append($"<h1>Hello, {this.GetUsername(request)}</h1>")
              .Append(File.ReadAllText("Views/" + "Index" + ".html").Replace("<p><a href=\"/login\">Login</a></p>", ""))
              .Append("<p><a href=\" /logout\">Logout</a></p>");
            return new HtmlResult(sb.ToString(), HttpResponseStatusCode.Ok);
        }
    }
}