using System.Threading.Tasks;
using Microsoft.Azure.Services.AppAuthentication;

namespace azuredevopsresourceanalyzer.core.Services
{
    public class AzureAdTokenService : ITokenService
    {
        public async Task<string> GetBearerToken(string resource)
        {
            var tokenProvider = new AzureServiceTokenProvider();
            var token = await tokenProvider.GetAccessTokenAsync(resource);
            return token;
        }
    }
}