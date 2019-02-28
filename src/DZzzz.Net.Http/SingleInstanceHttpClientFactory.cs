using System;
using System.Net.Http;

using DZzzz.Net.Http.Interfaces;

namespace DZzzz.Net.Http
{
    public class SingleInstanceHttpClientFactory : IHttpClientFactory
    {
        private readonly Lazy<HttpClient> httpClient;

        public SingleInstanceHttpClientFactory()
        {
            httpClient = new Lazy<HttpClient>(ValueFactory);
        }

        public HttpClient GetHttpClient()
        {
            return httpClient.Value;
        }

        protected virtual HttpClient ValueFactory()
        {
            return new HttpClient();
        }

        public void Dispose()
        {
            httpClient?.Value?.Dispose();
        }
    }
}