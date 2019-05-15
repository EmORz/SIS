using System;
using System.Text;
using SIS.HTTP.Common;
using SIS.HTTP.Enums;
using SIS.HTTP.Extensions;
using SIS.HTTP.Headers;
using SIS.HTTP.Headers.Contarct;
using SIS.HTTP.Response.Contracts;

namespace SIS.HTTP.Response
{
    public class HttpResponse : IHttpResponse
    {
        public HttpResponse()
        {
            this.Header = new HttpHeaderCollection();
            this.Content = new byte[0];
        }

        public HttpResponse(HttpResponseStatusCode statusCode) : this()
        {
            CoreValidator.ThrowIfFull(statusCode, nameof(statusCode));
            this.StatusCode = statusCode;
        }


        //todo implement
        public HttpResponseStatusCode StatusCode { get; set; }
        public IHttpHeaderCollection Header { get; }
        public byte[] Content { get; set; }
        public void AddHeader(HttpHeader header)
        {
            this.Header.AddHeader(header);
        }

        public byte[] GetBytes()
        {
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
            throw new NotImplementedException();
        }

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();

            sb.Append($"{GlobalConstants.HttpOneProtocolFragment} {this.StatusCode.GetStatusLine()}")
                .Append(GlobalConstants.HttpNewLine)
                .Append($"{this.Header}").Append(GlobalConstants.HttpNewLine);
            sb.Append(GlobalConstants.HttpNewLine);

            return sb.ToString();
        }

        public void AddHeaderr(HttpHeader header) => this.Header.AddHeader(header);


    }
}
