﻿using SIS.HTTP.Common;
using SIS.HTTP.Exceptions;
using SIS.HTTP.Requests;
using SIS.HTTP.Requests.Contracts;
using SIS.HTTP.Responses.Contracts;
using SIS.WebServer.Result;
using SIS.WebServer.Routing;
using SIS.WebServer.Routing.Contracts;
using System;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using Demo.App.Sessions;
using SIS.HTTP.Cookies;
using SIS.WebServer.Sessions;

namespace SIS.WebServer
{
    public class ConnectionHandler
    {
        private readonly Socket client;

        private readonly IServerRoutingTable serverRoutingTable;

        public ConnectionHandler(Socket client, IServerRoutingTable serverRoutingTable)
        {
            CoreValidator.ThrowIfNull(client, nameof(client));
            CoreValidator.ThrowIfNull(serverRoutingTable, nameof(serverRoutingTable));

            this.client = client;
            this.serverRoutingTable = serverRoutingTable;
        }

        

        private async Task<IHttpRequest> ReadRequestAsync()
        {
            var result = new StringBuilder();
            var data = new ArraySegment<byte>(new byte[1024]);

            while (true)
            {
                var numberOfBytesToRead = await this.client.ReceiveAsync(data.Array, SocketFlags.None);
                if (numberOfBytesToRead == 0)
                {
                    break;
                }
                var byteAsString = Encoding.UTF8.GetString(data.Array, 0, numberOfBytesToRead);
                result.Append(byteAsString);

                if (numberOfBytesToRead < 1023)
                {
                    break;
                }
            }
            if (result.Length == 0)
            {
                return null;
            }
            return new HttpRequest(result.ToString());
        }
        private IHttpResponse HandleRequest(IHttpRequest httpRequest)
        {
            if (!this.serverRoutingTable.Contains(httpRequest.RequestMethod, httpRequest.Path))
            {
                return new TextResult($"Route with method {httpRequest.RequestMethod} and path {httpRequest.Path} not found.", HTTP.Enums.HttpResponseStatusCode.NotFound);
            }
            return this.serverRoutingTable.Get(httpRequest.RequestMethod, httpRequest.Path).Invoke(httpRequest);
            
        }
        private void PrepareResponse(IHttpResponse httpResponse)
        {
            var byteSegment = httpResponse.GetBytes();
            this.client.Send(byteSegment, SocketFlags.None);
        }

        private string SetRequestSession(IHttpRequest httpRequest)
        {
            string sessionId = null;

            if (httpRequest.Cookies.ContainsCookie(HttpSessionStorage.SessionCookieKey))
            {
                var cookies = httpRequest.Cookies.GetCookie(HttpSessionStorage.SessionCookieKey);
                sessionId = cookies.Value;
            }
            else
            {
                sessionId = Guid.NewGuid().ToString();
            }
            httpRequest.Session = HttpSessionStorage.GetSession(sessionId);

            return httpRequest.Session.Id;
        }

        private void SetResponseSession(IHttpResponse httpResponse, string sessionId)
        {
            if (sessionId!=null)
            {
                httpResponse
                    .Cookies
                    .AddCookie
                        (new HttpCookie(HttpSessionStorage.SessionCookieKey, sessionId));
            }
        }


        public async Task ProcessRequestAsync()
        {
            IHttpResponse httpResponse = null;
            try
            {
                IHttpRequest httpRequest = await this.ReadRequestAsync();
                if (httpRequest != null)
                {
                    Console.WriteLine($"Processing: {httpRequest.RequestMethod} {httpRequest.Path}...");
                    var sessionId = this.SetRequestSession(httpRequest);
                    httpResponse = this.HandleRequest(httpRequest);
                    this.SetResponseSession(httpResponse, sessionId);
                }
            }
            catch (BadRequestException e)
            {
                httpResponse = new TextResult(e.Message, HTTP.Enums.HttpResponseStatusCode.BadRequest);
            }
            catch (Exception e)
            {
                httpResponse = new TextResult(e.Message, HTTP.Enums.HttpResponseStatusCode.InternalServerError);
            }
            this.PrepareResponse(httpResponse);
            this.client.Shutdown(SocketShutdown.Both);
        }
    }
}