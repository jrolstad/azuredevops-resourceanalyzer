using azuredevopsresourceanalyzer.core.Factories;
using azuredevopsresourceanalyzer.core.Managers;
using azuredevopsresourceanalyzer.core.Services;
using azuredevopsresourceanalyzer.ui.blazor.Application.ViewModels;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace azuredevopsresourceanalyzer.ui.blazor.Application.Configuration
{
    public class DependencyInjectionConfig
    {
        public static void Configure(IServiceCollection services, IConfiguration configuration)
        {
            services.AddTransient<ProjectSummaryViewModel>();
            services.AddTransient<ProjectSummaryManager>();
            services.AddTransient<AzureDevopsService>();
            services.AddTransient<ConfigurationService>();
            services.AddSingleton<IHttpClientFactory, StaticHttpClientFactory>();
        }
    }
}