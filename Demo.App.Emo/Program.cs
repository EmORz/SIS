using Demo.App.Emo.Contreller;
using SIS.HTTP.Enums;
using SIS.WebServer;
using SIS.WebServer.Result;
using SIS.WebServer.Routing;
using SIS.WebServer.Routing.Contracts;

namespace Demo.App.Emo
{
    public class Program
    {
        public static void Main(string[] args)
        {
            IServerRoutingTable serverRoutingTable = new ServerRoutingTable();

            serverRoutingTable.Add(SIS.HTTP.Enums.HttpRequestMethod.Get, "/", httpRequest => new HomeController().Home(httpRequest));

            //serverRoutingTable.Add(HttpRequestMethod.Get, "/", httpRequest =>
            //{
            //    return new HtmlResult("<h1>Hello World!</h1>", HttpResponseStatusCode.Ok);
            //});

            Server server = new Server(8000, serverRoutingTable);
            server.Run();
        }
    }
}
