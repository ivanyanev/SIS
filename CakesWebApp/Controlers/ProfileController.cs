using HTTP.Enums;
using HTTP.Requests;
using HTTP.Responses;
using System.Globalization;
using System.Linq;
using System.Text;
using WebServer.Results;

namespace CakesWebApp.Controlers
{
    public class ProfileController : BaseControler
    {
        public IHttpResponse MyProfile(IHttpRequest request)
        {
            var user = this.Db.Users.FirstOrDefault(x => x.Name == this.GetUsername(request));
            var sb = new StringBuilder();
            sb.Append("<a href=\" / \">Back to home</a>")
              .Append("<h1>My Profile</h1>")
              .Append($"<p>Name: {user.Name}</p>")
              .Append($"<p>Registered on: {user.DateOfRegistration.ToString(CultureInfo.InvariantCulture)}</p>")
              .Append($"<p>Orders Count: {user.Orders.Count()}</p>");

            return new HtmlResult(sb.ToString(), HttpResponseStatusCode.Ok);
            //return this.View("Profile");
        }
    }
}
