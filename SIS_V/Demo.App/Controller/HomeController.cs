using System;
using System.Collections.Generic;
using System.Text;
using SIS.HTTP.Requests.Contracts;
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
            return this.View();
        }
    }
}
