using System.Net.Http;
using System.Net.Http.Headers;
using azuredevopsresourceanalyzer.core.Services;

namespace azuredevopsresourceanalyzer.core.Factories
{
    public interface IHttpClientFactory
    {
        HttpClient Get();
    }
    public class StaticHttpClientFactory:IHttpClientFactory
    {
        private readonly ConfigurationService _configurationService;
        private static readonly HttpClient Client = new HttpClient();
        private string _accessToken = null;

        public StaticHttpClientFactory(ConfigurationService configurationService)
        {
            _configurationService = configurationService;
        }
        public HttpClient Get()
        {
            GetAuthenticationHeader();
            return Client;
        }

        private void GetAuthenticationHeader()
        {
            if (!string.IsNullOrWhiteSpace(_accessToken)) return;
            
            var azureAdTrustedResource = _configurationService.AzureAdTrustedResource();
            _accessToken = AzureAdTokenService.GetBearerToken(azureAdTrustedResource);

            Client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", _accessToken);

        }
    }
}