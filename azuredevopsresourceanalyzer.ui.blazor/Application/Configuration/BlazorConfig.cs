using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace azuredevopsresourceanalyzer.ui.blazor.Application.Configuration
{
    public class BlazorConfig
    {
        public static void Configure(IServiceCollection services, IConfiguration configuration)
        {
            services.AddRazorPages();
            services.AddServerSideBlazor();
        }

        public static void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
                endpoints.MapBlazorHub();
                endpoints.MapFallbackToPage("/_Host");
            });
        }
    }
}