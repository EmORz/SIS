using Demo.App.Emo.Contreller;
using SIS.HTTP.Enums;
using SIS.HTTP.Request;
using SIS.HTTP.Response;
using SIS.WebServer;
using SIS.WebServer.Result;
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

            var request =
                "POST /url/asd?name=john&id=1#fragment HTTP/1.1\r\n"
                + "Authorization: Basic 234567890\r\n"
                + "Date: " + DateTime.Now + "\r\n"
                + "Host: localhost:5000\r\n"
                + "\r\n"
                + "username=johndoe&password=123";

            HttpRequest httpRequestt = new HttpRequest(request);

            ;

            serverRoutingTable.Add(HttpRequestMethod.Get, "/", httpRequest => new HomeController().Home(httpRequest));

            //HttpResponse test = new HttpResponse();
            

            //serverRoutingTable.Add(HttpRequestMethod.Get, "/", httpRequest =>
            //{
            //    return new HtmlResult("<h1>Hello World!</h1>", HttpResponseStatusCode.Ok);
            //});

            Server server = new Server(8000, serverRoutingTable);
            server.Run();
        }
    }
}
