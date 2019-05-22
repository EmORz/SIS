using System;
using System.Collections.Generic;
using System.Text;
using SIS.HTTP.Cookies;
using SIS.HTTP.Requests.Contracts;
using SIS.HTTP.Responses;
using SIS.HTTP.Responses.Contracts;

namespace Demo.App.Controller
{
    public class HomeController : BaseController
    {
        public HomeController(IHttpRequest request)
        {
            this.httpRequest = request;

        }
        public IHttpResponse Index(IHttpRequest httpRequest)
        {
            IHttpResponse newrResponse = new HttpResponse();

            HttpCookie cookie = new HttpCookie("lang", "en");
            cookie.Delete();

            newrResponse.Cookies.AddCookie(cookie);

            return newrResponse;
        }

        public IHttpResponse Home(IHttpRequest httpRequest)
        {
            if (!this.IsLogged())
            {
                return this.Redirect($"/login");
            }

            this.ViewData["Username"] = this.httpRequest.Session.GetParameter("username");
            return this.View();


        }
    }
}
