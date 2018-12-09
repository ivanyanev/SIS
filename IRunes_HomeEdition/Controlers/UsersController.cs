using HTTP.Requests;
using HTTP.Responses;

namespace IRunes_HomeEdition.Controlers
{
    public class UsersController : BaseController
    {
        public IHttpResponse Register (IHttpRequest request)
        {
            return this.View("Users/Register");
        }
    }
}