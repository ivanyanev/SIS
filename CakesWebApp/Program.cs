using CakesWebApp.Controlers;
using CakesWebApp.Models;
using HTTP.Enums;
using System.Net;
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

            serverRoutingTable.Routes[HttpRequestMethod.Get]["/profile"] = request => new ProfileController().MyProfile(request);

            serverRoutingTable.Routes[HttpRequestMethod.Get]["/add"] = request => new CakesController().AddCakePage(request);

            serverRoutingTable.Routes[HttpRequestMethod.Post]["/add"] = request => new CakesController().AddCake(request);

            serverRoutingTable.Routes[HttpRequestMethod.Get]["/search"] = request => new CakesController().SearchPage(request);

            serverRoutingTable.Routes[HttpRequestMethod.Post]["/search"] = request => new CakesController().DoSearch(request);
            
            serverRoutingTable.Routes[HttpRequestMethod.Get]["/cakeDetails"] = request => new CakesController().GetCakeDetails(request);

            Server server = new Server(80, serverRoutingTable);

            server.Run();
        }
    }
}
