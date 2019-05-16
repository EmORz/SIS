using System;
using System.Collections.Generic;
using System.Text;
using SIS.HTTP.Enums;
using SIS.HTTP.Headers;
using SIS.HTTP.Response;

namespace SIS.WebServer.Result
{
    public class RedirectResult : HttpResponse
    {
        //todo: implement
        public RedirectResult(string location)
            : base(HttpResponseStatusCode.SeeOther)
        {
            this.Header.AddHeader(new HttpHeader("Location", location));
        }
    }
}
