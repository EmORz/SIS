﻿using System;
using SIS.HTTP.Enums;
using SIS.HTTP.Request.Contracts;
using SIS.HTTP.Response.Contracts;

namespace SIS.WebServer.Routing.Contracts
{
    public interface IServerRoutingTable
    {
        void Add(HttpRequestMethod method, string path, Func<IHttpRequest, IHttpResponse> func);

        bool Contains(HttpRequestMethod method, string path);

        Func<IHttpRequest, IHttpResponse> Get(HttpRequestMethod method, string path);
    }
}