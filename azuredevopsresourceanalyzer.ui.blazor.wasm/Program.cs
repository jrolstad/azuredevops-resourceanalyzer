using System;
using System.Net.Http;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Text;
using azuredevopsresourceanalyzer.ui.blazor.Application.Configuration;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace azuredevopsresourceanalyzer.ui.blazor.wasm
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebAssemblyHostBuilder.CreateDefault(args);
            builder.RootComponents.Add<App>("app");
            builder.Logging.SetMinimumLevel(LogLevel.Warning);

            DependencyInjectionConfig.Configure(builder);
            builder.Services.AddMsalAuthentication(options =>
            {
                builder.Configuration.Bind("AzureAd", options.ProviderOptions.Authentication);
                //options.ProviderOptions.DefaultAccessTokenScopes.Add("https://app.vssps.visualstudio.com/user_impersonation");
            });

            await builder.Build().RunAsync();
        }
    }
}
