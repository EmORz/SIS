using Demo.App.Controller;
using Demo.Data;
using SIS.HTTP.Enums;
using SIS.WebServer;
using SIS.WebServer.Routing;
using SIS.WebServer.Routing.Contracts;

namespace Demo.App
{
    public class StartUp
    {
        public static void Main(string[] args)
        {
            //Todo => it doesnt work - must be refactoring
            //using (var context = new DemoDbContext())
            //{
            //    context.Database.EnsureCreated();
            //}

            //

            IServerRoutingTable serverRoutingTable = new ServerRoutingTable();

            //serverRoutingTable.Add(HttpRequestMethod.Get, "/", httpRequest 
            //    => new HtmlResult("<h1>Hello World!</h1>", HttpResponseStatusCode.Ok));
            //;
            //[GET] Mapping
            serverRoutingTable.Add(HttpRequestMethod.Get, "/", httpRequest => new HomeController().Home(httpRequest));
            serverRoutingTable.Add(HttpRequestMethod.Get, "/login", httpRequest => new HomeController().Login(httpRequest));
            //serverRoutingTable.Add(HttpRequestMethod.Get, "/", httpRequest => new HomeController(httpRequest).Index(httpRequest));
            //serverRoutingTable.Add(HttpRequestMethod.Get, "/users/login", httpRequest => new UsersController().Login(httpRequest));
            //serverRoutingTable.Add(HttpRequestMethod.Get, "/users/register", httpRequest => new UsersController().Register(httpRequest));
            //serverRoutingTable.Add(HttpRequestMethod.Get, "/users/logout", httpRequest => new UsersController().Logout(httpRequest));

            //serverRoutingTable.Add(HttpRequestMethod.Get, "/home", httpRequest => new HomeController(httpRequest).Home(httpRequest));

            //[Post] Mapping
            serverRoutingTable.Add(HttpRequestMethod.Post, "/users/login", httpRequest => new UsersController().LoginConfirm(httpRequest));
            serverRoutingTable.Add(HttpRequestMethod.Post, "/users/register", httpRequest => new UsersController().RegisterConfrn(httpRequest));





            Server server = new Server(8000, serverRoutingTable);
            server.Run();
            //var request =
            //    "POST /url/asd?name=john&id=1#fragment HTTP/1.1\r\n"
            //    + "Authorization: Basic 234567890\r\n"
            //    + "Date: " + DateTime.Now + "\r\n"
            //    + "Host: localhost:5000\r\n"
            //    + "\r\n"
            //    + "username=johndoe&password=123";

            //HttpRequest httpRequest = new HttpRequest(request);
            //HttpResponse response = new HttpResponse(HttpResponseStatusCode.InternalServerError);
            //response.AddHeader(new HttpHeader("Host", "localhost:5000"));
            //response.AddHeader(new HttpHeader("Date", DateTime.Now.ToString(CultureInfo.InvariantCulture)));

            //response.Content = Encoding.UTF8.GetBytes("<h1>Hello World!</h1>");
            //Console.WriteLine(Encoding.UTF8.GetString(response.GetBytes()));


        }
    }
}
