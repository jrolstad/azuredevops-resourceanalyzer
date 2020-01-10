using Microsoft.AspNetCore.Components.Builder;

namespace azuredevopsresourceanalyzer.ui.blazorclient.Application.Configuration
{
    public static class BlazorConfig
    {
        public static void Configure(IComponentsApplicationBuilder app)
        {
            app.AddComponent<App>("app");
        }
    }
}