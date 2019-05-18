using SIS.HTTP.Common;
using SIS.HTTP.Exceptions;
using SIS.HTTP.Request;
using SIS.HTTP.Request.Contracts;
using SIS.HTTP.Response.Contracts;
using SIS.WebServer.Result;
using SIS.WebServer.Routing.Contracts;
using System;
using System.Net.Sockets;
using System.Text;

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

        private IHttpRequest ReadRequest()
        {
            var result = new StringBuilder();
            var data = new ArraySegment<byte>(new byte[1024]);

            while (true)
            {
                var numberOfBytesToRead = this.client.Receive(data.Array, SocketFlags.None);
                if (numberOfBytesToRead == 0 )
                {
                    break;
                }
                var byteAsString = Encoding.UTF8.GetString(data.Array, 0, numberOfBytesToRead);
                result.Append(byteAsString);

                if (numberOfBytesToRead<1023)
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
        private IHttpResponse HandleRequest( IHttpRequest httpRequest)
        {
            if (!this.serverRoutingTable.Contains(httpRequest.RequestMethod, httpRequest.Path))
            {
                return new TextResult($"Route with method {httpRequest.RequestMethod} and path {httpRequest.Path} not found.", HTTP.Enums.HttpResponseStatusCode.NotFound);
            }
            var result = this.serverRoutingTable.Get(httpRequest.RequestMethod, httpRequest.Path).Invoke(httpRequest);
            return result;
        }
        private void PrepareResponse(IHttpResponse httpResponse)
        {
            var byteSegment = httpResponse.GetBytes();
            this.client.Send(byteSegment, SocketFlags.None);
        }
        private void ProcessRequest()
        {
            IHttpResponse httpResponse = null;
            try
            {
                IHttpRequest httpRequest = this.ReadRequest();
                if (httpRequest != null)
                {
                    Console.WriteLine($"Processing: {httpRequest.RequestMethod} {httpRequest.Path}...");
                    httpResponse = this.HandleRequest(httpRequest);
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