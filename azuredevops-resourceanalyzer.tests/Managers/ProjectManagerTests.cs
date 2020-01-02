using System.Threading.Tasks;
using azuredevopsresourceanalyzer.core.Factories;
using azuredevopsresourceanalyzer.core.Managers;
using azuredevopsresourceanalyzer.core.Services;
using Xunit;

namespace azuredevops_resourceanalyzer.tests.Managers
{
    public class ProjectManagerTests
    {
        [Fact]
        public async Task Get_Organization_ReturnsProjects()
        {
            // Given
            var manager = Get();

            // When
            var result = await manager.Get("microsoftit");

            // Then
            Assert.NotEmpty(result);
        }

        private ProjectManager Get()
        {
            return new ProjectManager(new AzureDevopsService(new StaticHttpClientFactory(new ConfigurationService())));
        }
    }
}