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
            ConfigureRouting(serverRoutingTable);

            Server server = new Server(80, serverRoutingTable);

            server.Run();
        }

        private static void ConfigureRouting(ServerRoutingTable serverRoutingTable)
        {
            // GET
            serverRoutingTable.Routes[HttpRequestMethod.Get]["/home/index"] = request => new RedirectResult("/");
            serverRoutingTable.Routes[HttpRequestMethod.Get]["/"] = request => new HomeController().Index();
            serverRoutingTable.Routes[HttpRequestMethod.Get]["/users/login"] = request => new UsersController().Login(request);
            serverRoutingTable.Routes[HttpRequestMethod.Get]["/users/register"] = request => new UsersController().Register(request);

            // POST
            serverRoutingTable.Routes[HttpRequestMethod.Post]["/users/login"] = request => new UsersController().PostLogin(request);
            serverRoutingTable.Routes[HttpRequestMethod.Post]["/users/register"] = request => new UsersController().PostRegister(request);

        }
    }
}
