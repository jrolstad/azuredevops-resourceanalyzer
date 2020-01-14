using System.Threading.Tasks;
using azuredevopsresourceanalyzer.core.Services;
using Des.Blazor.Authorization.Msal;

namespace azuredevopsresourceanalyzer.ui.blazorclient.Application
{
    public class AzureAdUserTokenService:ITokenService
    {
        private readonly IAuthenticationManager _authenticationManager;

        public AzureAdUserTokenService(IAuthenticationManager authenticationManager)
        {
            _authenticationManager = authenticationManager;
        }
        public async Task<string> GetBearerToken(string resource)
        {
            var token = await _authenticationManager.GetAccessTokenAsync(new string[0]);
            return token?.AccessToken;
        }
    }
}