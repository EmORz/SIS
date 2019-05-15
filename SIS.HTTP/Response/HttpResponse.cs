using System;
using System.Collections.Generic;
using System.Text;
using SIS.HTTP.Enums;
using SIS.HTTP.Headers;
using SIS.HTTP.Headers.Contarct;
using SIS.HTTP.Response.Contracts;

namespace SIS.HTTP.Response
{
    public class HttpResponse : IHttpResponse
    {

        //todo implement
        public HttpResponseStatusCode StatusCode { get; set; }
        public IHttpHeaderCollection Header { get; }
        public byte[] Content { get; set; }
        public void AddHeader(HttpHeader header)
        {
            throw new NotImplementedException();
        }

        public byte[] GetBytes()
        {
            throw new NotImplementedException();
        }
    }
}
