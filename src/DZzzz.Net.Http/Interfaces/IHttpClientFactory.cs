using System.Net.Http;

namespace DZzzz.Net.Http.Interfaces
{
    public interface IHttpClientFactory
    {
        HttpClient GetHttpClient();
    }
}