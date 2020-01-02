using azuredevopsresourceanalyzer.ui.blazor.Application.Configuration;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace azuredevopsresourceanalyzer.ui.blazor
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        { 
            AuthenticationConfig.Configure(services,Configuration);
            BlazorConfig.Configure(services,Configuration);

            DependencyInjectionConfig.Configure(services, Configuration);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            MvcConfig.Configure(app,env);
            AuthenticationConfig.Configure(app,env);
            BlazorConfig.Configure(app,env);
        }
    }
}
