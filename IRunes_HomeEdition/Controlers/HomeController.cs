using HTTP.Requests;
using HTTP.Responses;

namespace IRunes_HomeEdition.Controlers
{
    public class HomeController : BaseController
    {
        public IHttpResponse Index(IHttpRequest request)
        {
            return this.View("Home/Index");
        }
    }
}
