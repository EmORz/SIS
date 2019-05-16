using Demo.App.Emo.Contreller;
using SIS.WebServer;
using SIS.WebServer.Routing;
using SIS.WebServer.Routing.Contracts;
using System;

namespace Demo.App.Emo
{
    public class Program
    {
        public static void Main(string[] args)
        {
            IServerRoutingTable serverRoutingTable = new ServerRoutingTable();

            serverRoutingTable.Add(SIS.HTTP.Enums.HttpRequestMethod.Get, "/", httpRequest => new HomeController().Home(httpRequest));


            Server server = new Server(8000, serverRoutingTable);
            server.Run();
        }
    }
}
