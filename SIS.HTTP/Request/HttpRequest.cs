using System;
using System.Collections.Generic;
using System.Text;
using SIS.HTTP.Common;
using SIS.HTTP.Enums;
using SIS.HTTP.Headers;
using SIS.HTTP.Headers.Contarct;
using SIS.HTTP.Request.Contracts;

namespace SIS.HTTP.Request
{
    public class HttpRequest : IHttpRequest
    {
        public HttpRequest(string requestString)
        {
           CoreValidator.ThrowIfNullOrEmpty(requestString, nameof(requestString));


           this.FormData = new Dictionary<string, object>();
           this.QueryData = new Dictionary<string, object>();
           this.Headers = new HttpHeaderCollection();
           
        }
        public string Path { get; private set; }

        public string Url { get; private set; }

        public Dictionary<string, object> FormData { get; }

        public Dictionary<string, object> QueryData { get; }

        public IHttpHeaderCollection Headers { get; }

        public HttpRequestMethod RequestMethod { get; private set; }

        //todo: add method!!!
    }
}
