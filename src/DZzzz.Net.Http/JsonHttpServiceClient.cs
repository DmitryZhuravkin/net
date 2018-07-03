using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;

using DZzzz.Net.Http.Configuration;
using DZzzz.Net.Http.Interfaces;

namespace DZzzz.Net.Http
{
    public class JsonHttpServiceClient : HttpServiceClient
    {
        public JsonHttpServiceClient(
            HttpServiceClientConfiguration configuration,
            IHttpClientFactory httpClientFactory)
            : base(configuration, httpClientFactory, Serializers.Json)
        {
        }

        protected override void InitDefaultRequestHeaders(HttpRequestHeaders headers, IDictionary<string, string> requestHeaders)
        {
            headers.Accept.TryParseAdd("application/json");

            base.InitDefaultRequestHeaders(headers, requestHeaders);
        }

        protected override void InitRequestHeaders(HttpRequestMessage request, IDictionary<string, string> contentHeaders)
        {
            if (request.Content != null)
            {
                request.Content.Headers.ContentType.MediaType = "application/json";
            }

            base.InitRequestHeaders(request, contentHeaders);
        }
    }
}