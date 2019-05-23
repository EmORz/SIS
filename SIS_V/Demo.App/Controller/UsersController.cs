using System.Linq;
using Demo.Data;
using Demo.Models;
using SIS.HTTP.Requests.Contracts;
using SIS.HTTP.Responses.Contracts;

namespace Demo.App.Controller
{
    public class UsersController : BaseController
    {
        public IHttpResponse Login(IHttpRequest httpRequest)
        {
            return this.View();
        }
        public IHttpResponse LoginConfirm(IHttpRequest httpRequest)
        {
            using (var context = new DemoDbContext())
            {

                var username = httpRequest.FormData["username"].ToString();
                var password = httpRequest.FormData["password"].ToString();

                User userFromDb = context.Users.SingleOrDefault(user => user.Username == username && user.Password == password);
                if (userFromDb == null)
                {
                    return this.Redirect("/login");
                }
                httpRequest.Session.AddParameter("username", userFromDb.Username);
                return this.Redirect("/home");


            }
        }
        public IHttpResponse Register(IHttpRequest httpRequest)
        {
            return this.View();
        }
        public IHttpResponse RegisterConfrn(IHttpRequest httpRequest)
        {
            using (var context = new DemoDbContext())
            {
                var username = httpRequest.FormData["username"].ToString();
                var password = httpRequest.FormData["password"].ToString();
                var conformPassword = httpRequest.FormData["confirmPassword"].ToString();

                if (conformPassword!=password)
                {
                    return this.Redirect("/register");
                }

                User user = new User
                {
                    Username =  username,
                    Password = password
                };

                context.Users.Add(user);
                context.SaveChanges();
            }
            return this.Redirect("/login");
        }

        public IHttpResponse Logout(IHttpRequest httpRequest)
        {
            if (this.IsLoggedIn())
            {
                httpRequest.Session.ClearParameters();
                return this.Redirect("/");
            }
            else
            {
                return this.Redirect("/");
            }
        }

    }
}