using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using DZzzz.Net.Core.Interfaces;
using DZzzz.Net.Http.Configuration;
using DZzzz.Net.Http.Interfaces;

namespace DZzzz.Net.Http
{
    public class HttpServiceClient
    {
        private readonly HttpServiceClientConfiguration configuration;
        private readonly IHttpClientFactory httpClientFactory;

        protected ISerializer<string> Serializer { get; }

        public HttpServiceClient(HttpServiceClientConfiguration configuration,
            IHttpClientFactory httpClientFactory,
            ISerializer<string> serializer)
        {
            this.configuration = configuration;
            this.httpClientFactory = httpClientFactory;

            Serializer = serializer;
        }

        public async Task<TK> SendRequestWithResultAsync<T, TK>(string relativeUrl, HttpMethod method, T data,
            IDictionary<string, string> contentHeaders = null, IDictionary<string, string> requestHeaders = null)
        {
            HttpClient client = httpClientFactory.GetHttpClient();
            Uri uri = BuildUri(relativeUrl);

            string strContent = String.Empty;

            if (data != null)
            {
                strContent = Serializer.Serialize(data);
            }

            InitRequestHeaders(client.DefaultRequestHeaders, requestHeaders);

            using (HttpRequestMessage request = new HttpRequestMessage(method, uri))
            {
                request.Content = new StringContent(strContent);

                InitContentHeaders(request.Content.Headers, contentHeaders);

                using (HttpResponseMessage message = await client.SendAsync(request).ConfigureAwait(false))
                {
                    await CheckResponse(message).ConfigureAwait(false);

                    string stringContent = await message.Content.ReadAsStringAsync().ConfigureAwait(false);

                    return Serializer.Deserialize<TK>(stringContent);
                }
            }
        }

        public Task<TK> SendRequestWithResultAsync<TK>(string relativeUrl, HttpMethod method,
            IDictionary<string, string> contentHeaders = null, IDictionary<string, string> requestHeaders = null)
        {
            return SendRequestWithResultAsync<object, TK>(relativeUrl, method, null, contentHeaders, requestHeaders);
        }

        public async Task SendRequestAsync<T>(string relativeUrl, HttpMethod method, T data,
            IDictionary<string, string> contentHeaders = null, IDictionary<string, string> requestHeaders = null)
        {
            HttpClient client = httpClientFactory.GetHttpClient();
            Uri uri = BuildUri(relativeUrl);

            string strContent = String.Empty;

            if (data != null)
            {
                strContent = Serializer.Serialize(data);
            }

            InitRequestHeaders(client.DefaultRequestHeaders, requestHeaders);

            using (HttpRequestMessage request = new HttpRequestMessage(method, uri))
            {
                request.Content = new StringContent(strContent);

                InitContentHeaders(request.Content.Headers, contentHeaders);

                using (HttpResponseMessage message = await client.SendAsync(request).ConfigureAwait(false))
                {
                    await CheckResponse(message).ConfigureAwait(false);
                }
            }
        }

        public Task SendRequestAsync(string relativeUrl, HttpMethod method,
            IDictionary<string, string> contentHeaders = null, IDictionary<string, string> requestHeaders = null)
        {
            return SendRequestAsync<object>(relativeUrl, method, null, contentHeaders, requestHeaders);
        }

        protected virtual Task CheckResponse(HttpResponseMessage response)
        {
            return Task.Run(() => response.EnsureSuccessStatusCode());
        }

        private void InitRequestHeaders(HttpRequestHeaders headers, IDictionary<string, string> requestHeaders)
        {
            InitHeaders(headers, requestHeaders);
        }

        protected virtual void InitContentHeaders(HttpContentHeaders headers, IDictionary<string, string> contentHeaders)
        {
            InitHeaders(headers, contentHeaders);
        }

        private void InitHeaders(HttpHeaders headers, IDictionary<string, string> clientHeaders)
        {
            if (clientHeaders != null && clientHeaders.Any())
            {
                foreach (KeyValuePair<string, string> clientHeader in clientHeaders)
                {
                    if (headers.Contains(clientHeader.Key))
                    {
                        headers.Remove(clientHeader.Key);
                    }

                    headers.Add(clientHeader.Key, clientHeader.Value);
                }
            }
        }

        private Uri BuildUri(string relativeUrl)
        {
            return configuration.BaseUrl.CombineAndConvertToUri(relativeUrl);
        }
    }
}