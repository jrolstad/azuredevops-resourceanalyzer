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
            return new TestCompositionRoot(new ServiceCollection(),true);
        }

        public static TestCompositionRoot CreateIntegration()
        {
            return new TestCompositionRoot(new ServiceCollection(), false);
        }

        private TestCompositionRoot(IServiceCollection services, bool useFakes)
        {

            DependencyInjectionConfig.Configure(services,new ConfigurationRoot(new List<IConfigurationProvider>()));
            if (useFakes)
            {
                RegisterFakes(services);
            }
            
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
        public Dictionary<string,List<GitRepository>> Repositories = new Dictionary<string, List<GitRepository>>();
        public Dictionary<string,List<BuildDefinition>> BuildDefinitions = new Dictionary<string, List<BuildDefinition>>();
        public Dictionary<string,List<ReleaseDefinition>> ReleaseDefinitions = new Dictionary<string, List<ReleaseDefinition>>();
        public Dictionary<string,List<GitPullRequest>> PullRequests = new Dictionary<string, List<GitPullRequest>>();
        public Dictionary<string,List<GitBranchStat>> BranchStats = new Dictionary<string, List<GitBranchStat>>();
        public Dictionary<string,List<GitCommitRef>> Commits = new Dictionary<string, List<GitCommitRef>>();
        public Dictionary<string,List<Release>> Releases = new Dictionary<string, List<Release>>();
        public Dictionary<string,List<Project>> Projects = new Dictionary<string,List<Project>>();
    }
}