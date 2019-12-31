using System;
using azuredevopsresourceanalyzer.core.Managers;
using azuredevopsresourceanalyzer.core.Services;
using Xunit;

namespace azuredevops_resourceanalyzer.tests.Managers
{
    public class ProjectSummaryManagerTests
    {
        [Fact]
        public void GetSummary_ValidInputs_CreatesSummary()
        {
            // Given
            var organization = "microsoftit";
            var project = "oneitvso";
            var filter = "sr-im";
            DateTime? startDate = null;
            var manager = GetInstance();

            // When
            var result = manager.GetSummary(organization, project, filter);

            // Then
            Assert.NotNull(result);
            Assert.Equal(organization,result.Organization);
            Assert.Equal(project,result.Project);
            Assert.Equal(filter,result.RepositoryFilter);
            Assert.Equal(startDate,result.StartDate);

            Assert.NotEmpty(result.Components);
        }

        private ProjectSummaryManager GetInstance()
        {
            return new ProjectSummaryManager(new AzureDevopsService(new ConfigurationService()));
        }
    }
}