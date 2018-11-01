using HTTP.Enums;
using IRunesWebApp.Controlers;
using WebServer;
using WebServer.Results;
using WebServer.Routing;

namespace IRunesWebApp
{
    public class Startup
    {
        public static void Main(string[] args)
        {
            ServerRoutingTable serverRoutingTable = new ServerRoutingTable();

            serverRoutingTable.Routes[HttpRequestMethod.Get]["/Home/Index"] = request => new RedirectResult("/");
            serverRoutingTable.Routes[HttpRequestMethod.Get]["/"] = request => new HomeController().Index();
            serverRoutingTable.Routes[HttpRequestMethod.Get]["/Users/Login"] = request => new UsersController().Login(request);
            serverRoutingTable.Routes[HttpRequestMethod.Get]["/Users/Register"] = request => new UsersController().Register(request);



            Server server = new Server(80, serverRoutingTable);

            server.Run();
        }
    }
}
