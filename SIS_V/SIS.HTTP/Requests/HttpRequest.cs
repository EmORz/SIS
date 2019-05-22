using SIS.HTTP.Common;
using SIS.HTTP.Enums;
using SIS.HTTP.Exceptions;
using SIS.HTTP.Headers;
using SIS.HTTP.Headers.Contracts;
using SIS.HTTP.Requests.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using Demo.App.Sessions.Contracts;
using SIS.HTTP.Cookies;
using SIS.HTTP.Cookies.Contracts;

namespace SIS.HTTP.Requests
{
    public class HttpRequest : IHttpRequest
    {
        public HttpRequest(string requestString)
        {
            CoreValidator.ThrowIfNullOrEmpty(requestString, nameof(requestString));

            this.FormData = new Dictionary<string, object>();
            this.QueryData = new Dictionary<string, object>();
            this.Headers = new HttpHeaderCollection();
            this.Cookies = new HttpCookieCollection();

            this.ParseRequest(requestString);
        }
        public string Path { get; set; }
        public string Url { get; set; }
        public Dictionary<string, object> FormData { get; }
        public Dictionary<string, object> QueryData { get; }
        public IHttpHeaderCollection Headers { get; }
        public IHttpCookieCollection Cookies { get; }
        public HttpRequestMethod RequestMethod { get; set; }
        public IHttpSession Session { get; set; }

        private bool IsValidRequestLine(string[] requestLineParams)
        {
            if (requestLineParams.Length != 3 || requestLineParams[2] != GlobalConstants.HttpOneProtocolFragment)
            {
                return false;
            }
            return true;
        }


        private bool HasQueryStrng()
        {
            return this.Url.Split('?').Length > 1;
        }

        private bool IsValidRequestQueryString(string queryString, string[] queryParameters)
        {
            CoreValidator.ThrowIfNullOrEmpty(queryString, nameof(queryString));
            return true;
        }

        private IEnumerable<string> ParsePlainRequestHeaders(string[] requestLines)
        {
            for (int i = 1; i < requestLines.Length - 1; i++)
            {
                if (!string.IsNullOrEmpty(requestLines[i]))
                {
                    yield return requestLines[i];
                }
            }
        }

        private void ParseRequestMethod(string[] requestLineParams)
        {
            var parseResult = HttpRequestMethod.TryParse(requestLineParams[0], true, out HttpRequestMethod method);

            if (!parseResult)
            {
                throw new BadRequestException(string.Format(GlobalConstants.UnsupportedHttpMethodExceptionMessage, requestLineParams[0]));
            }
            this.RequestMethod = method;
        }
        private void ParseRequestUrl(string[] requestLineParameter)
        {
            this.Url = requestLineParameter[1];
        }
        private void ParseRequestPath()
        {
            this.Path = this.Url.Split('?')[0];
        }

        private void ParseRequestHeaders(string[] plainHeaders)
        {
            plainHeaders
              .Select(plainHeader => plainHeader.Split(new[] { ": " }, StringSplitOptions.RemoveEmptyEntries))
              .ToList()
              .ForEach(headerKeyValuePair => this.Headers.AddHeader(new HttpHeader(headerKeyValuePair[0], headerKeyValuePair[1])));
        }
        private void ParseRequestQueryParameters()
        {
            if (HasQueryStrng())
            {
                this.Url.Split(new[] { '?', '#' })[1]
                    .Split('&')
                    .Select(plainQueryParameter => plainQueryParameter.Split('='))
                    .ToList()
                    .ForEach(queryParameterKeyValuePair => this.QueryData.Add(queryParameterKeyValuePair[0], queryParameterKeyValuePair[1]));
            }

        }
        private void ParseRequestFormDataParameters(string requestBody)
        {
            if (!string.IsNullOrEmpty(requestBody))
            {
                requestBody
                    .Split('&')
                    .Select(plainQueryParameter => plainQueryParameter.Split('='))
                    .ToList()
                    .ForEach(queryParameterKeyValuePair => this.FormData.Add(queryParameterKeyValuePair[0], queryParameterKeyValuePair[1]));
            }

        }
        private void ParseCookies()
        {
            if (this.Headers.ContainsHeader(HttpHeader.Cookie))
            {
                var value = this.Headers.GetHeader(HttpHeader.Cookie).Value;
                var unparseCookies = value.Split("; ", StringSplitOptions.RemoveEmptyEntries);


                foreach (var unparseCookie in unparseCookies)
                {
                    var cookieKeyValuePair = unparseCookie.Split("=", StringSplitOptions.RemoveEmptyEntries);
                    //Todo may be split by new[] {'='}, 2
                    HttpCookie httpCookie = new HttpCookie(cookieKeyValuePair[0], cookieKeyValuePair[1], false);
                    this.Cookies.AddCookie(httpCookie);
                }

            }
        }
        private void ParseRequestParameters(string requestBody)
        {
            this.ParseRequestQueryParameters();
            this.ParseRequestFormDataParameters(requestBody);
        }



        private void ParseRequest(string requestLineParams)
        {
            var splitRequestString = requestLineParams.Split(new[] { GlobalConstants.HttpNewLine }, StringSplitOptions.None);

            var requestLine = splitRequestString[0].Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);


            this.ParseRequestMethod(requestLine);

            this.ParseRequestUrl(requestLine);

            this.ParseRequestPath();

            this.ParseCookies();

            this.ParseRequestHeaders(ParsePlainRequestHeaders(splitRequestString).ToArray());

            this.ParseRequestParameters(splitRequestString[splitRequestString.Length - 1]);

        }

       
    }
}