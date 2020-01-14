using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components.Authorization;

namespace azuredevopsresourceanalyzer.ui.blazor.Application.ViewModels
{
    public class UserViewModel
    {
        private readonly AuthenticationStateProvider _authenticationStateProvider;

        public UserViewModel(AuthenticationStateProvider authenticationStateProvider)
        {
            _authenticationStateProvider = authenticationStateProvider;
        }

        public string Name { get; set; }
        public string AuthenticationType { get; set; }

        public List<KeyValuePair<string,string>> Claims { get; set; } = new List<KeyValuePair<string, string>>();

        public async Task Initialize()
        {
            var state = await _authenticationStateProvider.GetAuthenticationStateAsync();
            this.Name = state?.User?.Identity?.Name;
            this.AuthenticationType = state?.User?.Identity?.AuthenticationType;

            Claims = state.User?.Claims
                .Select(c => new KeyValuePair<string, string>(c.Type, c.Value))
                .ToList();
        }
    }
}