using System.Collections.Generic;
using azuredevopsresourceanalyzer.ui.blazor.Application.Configuration;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using azuredevopsresourceanalyzer.core.Models.AzureDevops;
using azuredevopsresourceanalyzer.core.Services;
using azuredevopsresourceanalyzer.ui.blazor.tests.TestUtility.Extensions;
using azuredevopsresourceanalyzer.ui.blazor.tests.TestUtility.Fakes;

namespace azuredevopsresourceanalyzer.ui.blazor.tests.TestUtility
{
    public class TestCompositionRoot
    {
        private readonly ServiceProvider _provider;

        public static TestCompositionRoot Create()
        {
            return new TestCompositionRoot(new ServiceCollection());
        }

        private TestCompositionRoot(IServiceCollection services)
        {

            DependencyInjectionConfig.Configure(services,new ConfigurationRoot(new List<IConfigurationProvider>()));
            RegisterFakes(services);

            _provider = services.BuildServiceProvider();
        }

        public TestContext Context => _provider.GetService<TestContext>();

        private void RegisterFakes(IServiceCollection services)
        {
            services.AddSingleton<TestContext>();
            services.ReplaceSingleton<IAzureDevopsService, FakeAzureDevOpsService>();
        }
        public T Get<T>()
        {
            return _provider.GetService<T>();
        }
    }

    public class TestContext
    {
        public List<GitRepository> Repositories = new List<GitRepository>();
        public List<Project> Projects = new List<Project>();
    }
}