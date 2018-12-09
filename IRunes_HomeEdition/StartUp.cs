using HTTP.Enums;
using IRunes_HomeEdition.Controlers;
using WebServer;
using WebServer.Results;
using WebServer.Routing;

namespace IRunes_HomeEdition
{
    public class StartUp
    {
        public static void Main(string[] args)
        {
            ServerRoutingTable serverRoutingTable = new ServerRoutingTable();

            serverRoutingTable.Routes[HttpRequestMethod.Get]["/home/index"] = request => new RedirectResult("/");
            serverRoutingTable.Routes[HttpRequestMethod.Get]["/"] = request => new HomeController().Index(request);


            //serverRoutingTable.Routes[HttpRequestMethod.Post]["/users/login"] = request => new AccountController().Login(request);

            serverRoutingTable.Routes[HttpRequestMethod.Get]["/users/register"] = request => new UsersController().Register(request);



            Server server = new Server(80, serverRoutingTable);

            server.Run();
        }
    }
}