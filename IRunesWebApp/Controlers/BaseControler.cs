using CakesWebApp.Services;
using HTTP.Cookies;
using HTTP.Enums;
using HTTP.Requests;
using HTTP.Responses;
using IRunesWebApp.Data;
using System.IO;
using System.Runtime.CompilerServices;
using WebServer.Results;

namespace IRunesWebApp.Controlers
{
    public abstract class BaseControler
    {
        private readonly UserCookieService userCookieService;

        private const string RootDirectoryRelativePath = "../../../";

        private const string ViewsFolderName = "Views";

        private const string ControllerDefaultName = "Controller";

        private const string HtmlFileExtension = ".html";

        private const string DirectorySeparator = "/";

        public BaseControler()
        {
            this.Context = new IRunesDbContext();
            this.userCookieService = new UserCookieService();
        }

        public void SignInUser(string username, IHttpResponse response, IHttpRequest request)
        {
            request.Session.AddParameter("username", username);
            var userCookieValue = userCookieService.GetUserCookie(username);
            response.Cookies.Add(new HttpCookie("IRunes_auth", userCookieValue));
        }

        protected IRunesDbContext Context { get; private set; }

        private string GetCurrentControllerName() =>
            this.GetType().Name.Replace(ControllerDefaultName, string.Empty);

        protected IHttpResponse View([CallerMemberName] string viewName = "")
        {
            string filePath = RootDirectoryRelativePath +
                ViewsFolderName +
                DirectorySeparator +
                this.GetCurrentControllerName() +
                DirectorySeparator +
                viewName +
                HtmlFileExtension;

            if (!File.Exists(filePath))
            {
                return new BadRequestResult($"View {viewName} not found", HttpResponseStatusCode.NotFound);
            }

            var fileContent = File.ReadAllText(filePath);
            var response = new HtmlResult(fileContent, HttpResponseStatusCode.Ok);

            return response;
        }
    }
}
