using Microsoft.Azure.Services.AppAuthentication;

namespace azuredevopsresourceanalyzer.core.Services
{
    public class AzureAdTokenService
    {
        public static string GetBearerToken(string resource)
        {
            var tokenProvider = new AzureServiceTokenProvider();
            var token = tokenProvider.GetAccessTokenAsync(resource).Result;
            return token;
        }
    }
}