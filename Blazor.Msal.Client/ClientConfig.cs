using Des.Blazor.Authorization.Msal;

namespace azuredevopsresourceanalyzer.ui.blazorclient
{
    public class ClientConfig : IMsalConfig
    {
        public string Authority { get; set; }
        public string ClientId { get; set; }
        public LoginModes LoginMode => LoginModes.Redirect;
    }
}
