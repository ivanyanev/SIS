using CakesWebApp.Data;
using CakesWebApp.Services;
using HTTP.Enums;
using HTTP.Requests;
using HTTP.Responses;
using System.Collections.Generic;
using System.IO;
using WebServer.Results;

namespace CakesWebApp.Controlers
{
    public abstract class BaseControler
    {
        protected BaseControler()
        {
            this.Db = new CakesDbContext();
            this.UserCookieService = new UserCookieService();
        }

        protected CakesDbContext Db { get; }

        protected IUserCookieService UserCookieService { get; }

        protected string GetUsername(IHttpRequest request)
        {
            if (!request.Cookies.ContainsCookie(".auth-cakes"))
            {
                return null;
            }
            var cookie = request.Cookies.GetCookie(".auth-cakes");
            var cookieContent = cookie.Value;
            var userName = this.UserCookieService.GetUserData(cookieContent);

            return userName;
        }

        protected IHttpResponse View(string viewName)
        {
            var allContent = this.GetViewContent(viewName, new Dictionary<string, string>());

            return new HtmlResult(allContent, HttpResponseStatusCode.Ok);
        }

        protected IHttpResponse BadRequestError(string errorMessage)
        {
            var viewBag = new Dictionary<string, string>();
            viewBag.Add("Error", errorMessage);
            var allContent = this.GetViewContent("Error", viewBag);

            return new HtmlResult(allContent, HttpResponseStatusCode.BadRequest);
        }

        protected IHttpResponse ServertError(string errorMessage)
        {
            var viewBag = new Dictionary<string, string>();
            viewBag.Add("Error", errorMessage);
            var allContent = this.GetViewContent("Error", viewBag);

            return new HtmlResult(allContent, HttpResponseStatusCode.InternalServerError);
        }

        protected string GetViewContent(string viewName, IDictionary<string, string> viewBag)
        {
            var layoutContent = File.ReadAllText("Views/_Layout.html");
            var content = File.ReadAllText("Views/" + viewName + ".html");
            foreach (var item in viewBag)
            {
                content = content.Replace("@Model." + item.Key, item.Value);
            }
            var allContent = layoutContent.Replace("@RenderBody()", content);

            return allContent;
        }
    }
}
