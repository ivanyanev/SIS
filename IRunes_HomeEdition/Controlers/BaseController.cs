using HTTP.Enums;
using HTTP.Requests;
using HTTP.Responses;
using IRunes_HomeEdition.Data;
using IRunes_HomeEdition.Services;
using System.IO;
using WebServer.Results;

namespace IRunes_HomeEdition.Controlers
{
    public class BaseController
    {
        public BaseController()
        {
            this.Db = new IRunesDbContext();
            this.UserCookiesService = new UserCookieService();
        }

        protected IRunesDbContext Db { get; }

        protected IUserCookieService UserCookiesService { get; }

        protected string GetUsername(IHttpRequest request)
        {
            if (!request.Cookies.ContainsCookie(".auth-irunes"))
            {
                return null;
            }

            var cookie = request.Cookies.GetCookie(".auth-irunes");
            var cookieContent = cookie.Value;
            var userName = this.UserCookiesService.GetUserData(cookieContent);

            return userName;
        }

        protected IHttpResponse View(string viewName)
        {
            var content = File.ReadAllText("Views/" + viewName + ".html");

            return new HtmlResult(content, HttpResponseStatusCode.Ok);
        }

        protected IHttpResponse BadRequestError(string errorMessage)
        {
            return new HtmlResult($"<h1>{errorMessage}</h1>", HttpResponseStatusCode.BadRequest);
        }

        protected IHttpResponse ServertError(string errorMessage)
        {
            return new HtmlResult($"<h1>{errorMessage}</h1>", HttpResponseStatusCode.InternalServerError);
        }
    }
}
