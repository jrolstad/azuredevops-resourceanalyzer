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
            services.AddHttpClient();

            services.AddTransient<ProjectSummaryViewModel>();
            services.AddTransient<WorkSummaryViewModel>();

            services.AddTransient<ProjectSummaryManager>();
            services.AddTransient<ProjectManager>();
            services.AddTransient<WorkSummaryManager>();

            services.AddSingleton<IAzureDevopsService,AzureDevopsService>();
            services.AddSingleton<ConfigurationService>();
        }
    }
}