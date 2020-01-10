using Des.Blazor.Authorization.Msal;
using Microsoft.AspNetCore.Components.Builder;
using Microsoft.Extensions.DependencyInjection;

namespace azuredevopsresourceanalyzer.ui.blazorclient
{
    public class Startup
    {
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddAuthorizationCore();

            var myConfig = new MsalConfig
            {
                ClientId = "b08edd15-e207-4dd4-86af-44d13723b9b6",
                Authority = "https://login.microsoftonline.com/72f988bf-86f1-41af-91ab-2d7cd011db47",
                LoginMode = LoginModes.Redirect
            };
            services.AddAzureActiveDirectory(myConfig);
        }

        public void Configure(IComponentsApplicationBuilder app)
        {
            app.AddComponent<App>("app");
        }
    }

    public class MsalConfig : IMsalConfig
    {
        public string ClientId { get; set; }
        public string Authority { get; set; }
        public LoginModes LoginMode { get; set; }
    }
}
