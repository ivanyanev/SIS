using HTTP.Responses;

namespace IRunesWebApp.Controlers
{
    public class HomeController : BaseControler
    {
        public IHttpResponse Index() => this.View();
    }
}