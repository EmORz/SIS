using SIS.HTTP.Request.Contracts;
using SIS.HTTP.Response.Contracts;
using System;
using System.Collections.Generic;
using System.Text;

namespace Demo.App.Emo.Contreller
{
    public class HomeController : BaseController
    {
        public IHttpResponse Home(IHttpRequest httpRequest)
        {
            return this.View();
        }
    }
}
