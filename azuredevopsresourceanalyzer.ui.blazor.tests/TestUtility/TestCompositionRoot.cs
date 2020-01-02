using System.Collections.Generic;
using azuredevopsresourceanalyzer.ui.blazor.Application.Configuration;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

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

            _provider = services.BuildServiceProvider();
        }

        public TestContext Context => _provider.GetService<TestContext>();

        public T Get<T>()
        {
            return _provider.GetService<T>();
        }
    }

    public class TestContext
    {
    }
}