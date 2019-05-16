using SIS.HTTP.Common;
using SIS.HTTP.Request;
using SIS.HTTP.Request.Contracts;
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
    }
}