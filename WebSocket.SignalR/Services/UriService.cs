﻿using Microsoft.AspNetCore.WebUtilities;
using WebSocket.SignalR.Interfaces;

namespace WebSocket.SignalR.Services
{
    public class UriService : IUriService
    {
        private readonly string _baseUri;
        public UriService(string baseUri)
        {
            _baseUri = baseUri;
        }
        public Uri GetPageUri(int pageIndex, int pageSize, string route)
        {
            var _endpointUri = new Uri(string.Concat(_baseUri, route));
            var modifiedUri = QueryHelpers.AddQueryString(_endpointUri.ToString(), "pageIndex", pageIndex.ToString());
            modifiedUri = QueryHelpers.AddQueryString(modifiedUri, "pageSize", pageSize.ToString());
            return new Uri(modifiedUri);
        }

        public Uri GetUri(string route)
        {
            return new Uri(string.Concat(_baseUri, route));
        }
    }
}
