using System;
using System.Threading.Tasks;
using azuredevopsresourceanalyzer.core.Services;
using Des.Blazor.Authorization.Msal;
using Microsoft.IdentityModel.Clients.ActiveDirectory;

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
            for (var step = 0; step < 3; step++)
            {
                var token = await _authenticationManager.GetAccessTokenAsync("");

                if (!string.IsNullOrWhiteSpace(token?.AccessToken))
                {
                    return token?.AccessToken;
                }
            }

            return null;

        }
    }
}