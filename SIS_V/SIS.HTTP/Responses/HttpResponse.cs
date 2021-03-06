﻿using System.Linq;
using System.Text;
using SIS.HTTP.Common;
using SIS.HTTP.Cookies;
using SIS.HTTP.Cookies.Contracts;
using SIS.HTTP.Enums;
using SIS.HTTP.Extensions;
using SIS.HTTP.Headers;
using SIS.HTTP.Headers.Contracts;
using SIS.HTTP.Responses.Contracts;

namespace SIS.HTTP.Responses
{
    public class HttpResponse : IHttpResponse
    {

        public HttpResponse()
        {
            this.Header = new HttpHeaderCollection();
            this.Cookies = new HttpCookieCollection();
            this.Content = new byte[0];
        }

        public HttpResponse(HttpResponseStatusCode statusCode) : this()
        {
            CoreValidator.ThrowIfNull(statusCode, nameof(statusCode));
            this.StatusCode = statusCode;
        }
        public HttpResponseStatusCode StatusCode { get; set; }
        public IHttpHeaderCollection Header { get; }
        public IHttpCookieCollection Cookies { get; }
        public byte[] Content { get; set; }
        public void AddHeader(HttpHeader header)
        {
            this.Header.AddHeader(header);
        }

        public void AddCookie(HttpCookie cookie)
        {
            this.Cookies.AddCookie(cookie);
        }

        public byte[] GetBytes()
        {
            //short variant, but is too slowly
           // var httpResponseBytesWithBody = Encoding.UTF8.GetBytes(this.ToString()).Concat(this.Content).ToArray();

            var httpResponseBytesWithoutBody = Encoding.UTF8.GetBytes(this.ToString());

            var httpResponseBytesWithBody = new byte[httpResponseBytesWithoutBody.Length + this.Content.Length];

            for (int i = 0; i < httpResponseBytesWithoutBody.Length; i++)
            {
                httpResponseBytesWithBody[i] = httpResponseBytesWithoutBody[i];
            }

            for (int i = 0; i < httpResponseBytesWithBody.Length - httpResponseBytesWithoutBody.Length; i++)
            {
                httpResponseBytesWithBody[i + httpResponseBytesWithoutBody.Length] = this.Content[i];
            }
            return httpResponseBytesWithBody;
        }

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();

            sb.Append($"{GlobalConstants.HttpOneProtocolFragment} {this.StatusCode.GetStatusLine()}")
                .Append(GlobalConstants.HttpNewLine)
                .Append($"{this.Header}").Append(GlobalConstants.HttpNewLine);

            if (this.Cookies.HasCookies())
            {
                sb.Append($"{this.Cookies}").Append(GlobalConstants.HttpNewLine); 
            }
            sb.Append(GlobalConstants.HttpNewLine);
            return sb.ToString();
        }
    }
}