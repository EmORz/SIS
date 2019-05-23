using SIS.HTTP.Responses.Contracts;
using SIS.WebServer.Result;
using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.CompilerServices;
using SIS.HTTP.Cookies;
using SIS.HTTP.Enums;
using SIS.HTTP.Requests.Contracts;

namespace Demo.App.Controller
{
    public abstract class BaseController
    {
        protected IHttpRequest httpRequest { get; set; }

        public IHttpResponse View([CallerMemberName]string view = null)
        {
            var controllerName = this.GetType().Name.Replace("Controller", String.Empty);
            string viewName = view;


            var viewContent = File.ReadAllText("Views/" + controllerName + "/" + viewName + ".html");

            viewContent = ParseTemplate(viewContent);

            var result = new HtmlResult(viewContent, HttpResponseStatusCode.Ok);
            result.Cookies.AddCookie(new HttpCookie("lang", "en"));
            return result;
        }

        protected bool IsLoggedIn()
        {
            return httpRequest.Session.ContainsParameter("username");
        }
        public IHttpResponse Redirect(string url)
        {
            return new RedirectResult(url);
        }

        protected Dictionary<string, object> ViewData = new Dictionary<string, object>();

        private string ParseTemplate(string viewContent)
        {
            //if (this.IsLoggedIn())
            //{
                viewContent.Replace("@Model.HelloMessage", $"HelLLlo {this.httpRequest.Session.GetParameter("username")}!");

            
            //else
            //{
            //    viewContent.Replace("@Model.HelloMessage", "Hello U World from SIS.WebServer! E.");

            //}
            //foreach (var param in this.ViewData)
            //{
            //    viewContent = viewContent.Replace($"{param.Key}", param.Value.ToString());
            //}
            return viewContent;
        }


    }
}