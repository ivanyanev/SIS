using CakesWebApp.Controlers;
using HTTP.Enums;
using WebServer;
using WebServer.Routing;

namespace CakesWebApp
{
    public class Program
    {
        static void Main(string[] args)
        {
            ServerRoutingTable serverRoutingTable = new ServerRoutingTable();

            serverRoutingTable.Routes[HttpRequestMethod.Get]["/"] = request => new HomeController().Index(request);

            serverRoutingTable.Routes[HttpRequestMethod.Get]["/register"] = request => new AccountControler().Register(request);
            
            serverRoutingTable.Routes[HttpRequestMethod.Get]["/login"] = request => new AccountControler().Login(request);

            serverRoutingTable.Routes[HttpRequestMethod.Post]["/register"] = request => new AccountControler().DoRegister(request);

            serverRoutingTable.Routes[HttpRequestMethod.Post]["/login"] = request => new AccountControler().DoLogin(request);

            serverRoutingTable.Routes[HttpRequestMethod.Get]["/hello"] = request => new HomeController().HelloUser(request);

            serverRoutingTable.Routes[HttpRequestMethod.Get]["/logout"] = request => new AccountControler().Logout(request);

            Server server = new Server(80, serverRoutingTable);

            server.Run();
        }
    }
}
