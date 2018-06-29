using System;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

using DZzzz.Net.Core.Interfaces;
using DZzzz.Net.Core.Logging;

namespace DZzzz.Net.Http
{
    public class HttpLoggingHandler : DelegatingHandler
    {
        private readonly ILogger logger;

        public HttpLoggingHandler(HttpMessageHandler innerHandler, ILogger logger)
            : base(innerHandler)
        {
            this.logger = logger;
        }

        protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            await Log($"Request: {request.RequestUri}{Environment.NewLine}{request}", request.Content);

            HttpResponseMessage response = await base.SendAsync(request, cancellationToken);

            await Log($"Response:{Environment.NewLine}{response}", response.Content);

            return response;
        }

        private async Task Log(string item, HttpContent content)
        {
            if (logger != null)
            {
                StringBuilder builder = new StringBuilder();
                builder.AppendLine(item);

                if (content != null)
                {
                    builder.AppendLine(await content.ReadAsStringAsync());
                }

                logger.Write<HttpLoggingHandler>(builder.ToString(), LogLevel.Debug);
            }
        }
    }
}