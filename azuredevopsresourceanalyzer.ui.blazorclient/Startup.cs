using Microsoft.AspNetCore.Components.Builder;
using Microsoft.Extensions.DependencyInjection;
using System;
using azuredevopsresourceanalyzer.ui.blazorclient.Application.Configuration;

namespace azuredevopsresourceanalyzer.ui.blazorclient
{
    public class Startup
    {
        public void ConfigureServices(IServiceCollection services)
        {
            AuthenticationConfig.Configure(services);
        }

        public void Configure(IComponentsApplicationBuilder app)
        {
           BlazorConfig.Configure(app);
        }
    }
}
