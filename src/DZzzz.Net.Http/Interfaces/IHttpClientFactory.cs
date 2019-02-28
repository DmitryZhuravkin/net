using System;
using System.Net.Http;

namespace DZzzz.Net.Http.Interfaces
{
    public interface IHttpClientFactory : IDisposable
    {
        HttpClient GetHttpClient();
    }
}