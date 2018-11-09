using HTTP.Requests;
using HTTP.Responses;

namespace IRunes_HomeEdition.Controlers
{
    public class AccountController : BaseController
    {
        public IHttpResponse Register (IHttpRequest request)
        {
            return this.View("Register");
        }
    }
}