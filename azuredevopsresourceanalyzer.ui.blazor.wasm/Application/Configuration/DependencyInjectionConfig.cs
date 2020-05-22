using System;
using System.Net.Http;
using azuredevopsresourceanalyzer.core.Managers;
using azuredevopsresourceanalyzer.core.Services;
using azuredevopsresourceanalyzer.ui.blazor.Application.ViewModels;
using Microsoft.AspNetCore.Components.WebAssembly.Authentication;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace azuredevopsresourceanalyzer.ui.blazor.Application.Configuration
{
    public class DependencyInjectionConfig
    {
        public static void Configure(WebAssemblyHostBuilder builder)
        {
            RegisterDefaultHttpClient(builder);
            RegisterAzureDevOpsHttpClient(builder);

            builder.Services.AddTransient<ProjectSummaryViewModel>();
            builder.Services.AddTransient<WorkSummaryViewModel>();

            builder.Services.AddTransient<ProjectSummaryManager>();
            builder.Services.AddTransient<ProjectManager>();
            builder.Services.AddTransient<WorkSummaryManager>();

            builder.Services.AddSingleton<IAzureDevopsService,AzureDevopsService>();
            builder.Services.AddSingleton<ConfigurationService>();
        }

        private static void RegisterDefaultHttpClient(WebAssemblyHostBuilder builder)
        {
            builder.Services.AddTransient(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });
        }

        private static void RegisterAzureDevOpsHttpClient(WebAssemblyHostBuilder builder)
        {
            builder.Services.AddHttpClient("AzureDevOpsApi", client =>
                    client.BaseAddress = new Uri("https://dev.azure.com"))
            .AddHttpMessageHandler(sp => sp.GetRequiredService<AuthorizationMessageHandler>()
                .ConfigureHandler(new[] { "https://dev.azure.com" }, new[] { "https://app.vssps.visualstudio.com/user_impersonation" }));


        }
    }
}