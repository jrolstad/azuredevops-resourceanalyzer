using System;
using System.Linq;
using System.Threading.Tasks;
using azuredevopsresourceanalyzer.ui.blazor.Application.ViewModels;
using azuredevopsresourceanalyzer.ui.blazor.tests.TestUtility;
using Xunit;

namespace azuredevopsresourceanalyzer.ui.blazor.tests.IntegrationTests
{
    public class WorkSummaryViewModelTests
    {
        [Fact]
        [Trait("Category", "Integration")]
        public async Task Search_ValidOrganizationAndProject_GetsTeams()
        {
            // Given
            var root = TestCompositionRoot.CreateIntegration();

            var viewModel = root.Get<WorkSummaryViewModel>();

            viewModel.Organization = "microsoftit";
            viewModel.Project = "oneitvso";
            viewModel.TeamsFilter = "all treasury";
            viewModel.StartDate = new DateTime(2020,1,1);

            // When
            await viewModel.Search();

            // Then
            Assert.Null(viewModel.Error);
            Assert.NotEmpty(viewModel.Results);
        }

        [Fact]
        [Trait("Category", "Integration")]
        public async Task SearchProjects_ValidOrganization_GetsProjects()
        {
            // Given
            var root = TestCompositionRoot.CreateIntegration();

            var viewModel = root.Get<WorkSummaryViewModel>();

            viewModel.Organization = "jrolstad";

            // When
            await viewModel.SearchProjects();

            // Then
            Assert.Null(viewModel.Error);
            Assert.NotEmpty(viewModel.Projects);
            Assert.Equal(viewModel.Projects.First(), viewModel.Project);
        }
    }
}