using azuredevopsresourceanalyzer.core.Managers;
using azuredevopsresourceanalyzer.core.Services;
using azuredevopsresourceanalyzer.ui.blazor.Application.ViewModels;
using Microsoft.Extensions.DependencyInjection;

namespace azuredevopsresourceanalyzer.ui.blazorclient.Application.Configuration
{
    public class DependencyInjectionConfig
    {
        public static void Configure(IServiceCollection services)
        {
            services.AddHttpClient();

            services.AddTransient<ProjectSummaryViewModel>();
            services.AddTransient<WorkSummaryViewModel>();
            services.AddTransient<UserViewModel>();

            services.AddTransient<ProjectSummaryManager>();
            services.AddTransient<ProjectManager>();
            services.AddTransient<WorkSummaryManager>();

            services.AddSingleton<IAzureDevopsService,AzureDevopsService>();
            services.AddSingleton<ConfigurationService>();
            services.AddSingleton<ITokenService,AzureAdUserTokenService>();
        }
    }
}