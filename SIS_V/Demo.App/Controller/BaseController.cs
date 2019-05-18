using SIS.HTTP.Responses.Contracts;
using SIS.WebServer.Result;
using System;
using System.IO;
using System.Runtime.CompilerServices;

namespace Demo.App.Controller
{
    public abstract class BaseController
    {
        public IHttpResponse View([CallerMemberName]string view = null)
        {
            var controllerName = this.GetType().Name.Replace("Controller", String.Empty);
            var viewName = view;

            var viewContent = File.ReadAllText("Views/" + controllerName + "/" + viewName + ".html");
            var result = new HtmlResult(viewContent, SIS.HTTP.Enums.HttpResponseStatusCode.Ok);
            return result;

        }
    }
}