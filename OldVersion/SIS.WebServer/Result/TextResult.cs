using System;
using System.Collections.Generic;
using System.Text;
using SIS.HTTP.Enums;
using SIS.HTTP.Headers;
using SIS.HTTP.Response;

namespace SIS.WebServer.Result
{
    public class TextResult : HttpResponse
    {

        public TextResult(string content, HttpResponseStatusCode responseStatusCode,
            string contentType = "text/plain; charset=utf-8"): 
            base(responseStatusCode)
        {
            this.Header.AddHeader(new HttpHeader("Content-Type", contentType));
            this.Content = Encoding.UTF8.GetBytes(content);
        }

        public TextResult(byte[] content, HttpResponseStatusCode responseStatusCode,
         string contentType = "text/plain; charset=utf-8") :
         base(responseStatusCode)
        {
            this.Header.AddHeader(new HttpHeader("Content-Type", contentType));
            this.Content = content;
        }
    }
}
