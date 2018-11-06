using HTTP.Enums;
using IRunes_HomeEdition.Controlers;
using WebServer;
using WebServer.Routing;

namespace IRunes_HomeEdition
{
    public class StartUp
    {
        public static void Main(string[] args)
        {
            ServerRoutingTable serverRoutingTable = new ServerRoutingTable();

            serverRoutingTable.Routes[HttpRequestMethod.Get]["/"] = request => new HomeController().Index(request);

            Server server = new Server(80, serverRoutingTable);

            server.Run();
        }
    }
}