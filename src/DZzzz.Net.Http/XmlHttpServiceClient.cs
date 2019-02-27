using DZzzz.Net.Http.Configuration;
using DZzzz.Net.Http.Interfaces;

namespace DZzzz.Net.Http
{
    public class XmlHttpServiceClient : HttpServiceClient
    {
        public XmlHttpServiceClient(
            HttpServiceClientConfiguration configuration,
            IHttpClientFactory httpClientFactory)
            : base(configuration, httpClientFactory, Serializers.Xml)
        {
        }
    }
}