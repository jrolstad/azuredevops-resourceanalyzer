using System;
using Microsoft.AspNetCore.Components.Builder;
using Microsoft.Extensions.DependencyInjection;

namespace azuredevopsresourceanalyzer.ui.blazorclient.Application.Configuration
{
    public static class AuthenticationConfig
    {
        public static void Configure(IServiceCollection services)
        {
            services.AddAuthorizationCore();
            services.AddAzureActiveDirectory(
                new Uri($"/config/appsettings.json?{DateTime.Now.Ticks}", UriKind.Relative));
        }

    }
}