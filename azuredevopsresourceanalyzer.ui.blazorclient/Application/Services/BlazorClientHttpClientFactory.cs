using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace azuredevopsresourceanalyzer.ui.blazorclient.Application.Services
{
    public class BlazorClientHttpClientFactory:IHttpClientFactory
    {
        private readonly HttpClient _client;

        public BlazorClientHttpClientFactory(HttpClient client)
        {
            _client = client;
        }
        public HttpClient CreateClient(string name)
        {
            return _client;
        }
    }
}
