using System;
using System.Linq;
using System.Threading.Tasks;
using azuredevopsresourceanalyzer.ui.blazor.Application.ViewModels;
using azuredevopsresourceanalyzer.ui.blazor.tests.TestUtility;
using azuredevopsresourceanalyzer.ui.blazor.tests.TestUtility.Extensions;
using Xunit;

namespace azuredevopsresourceanalyzer.ui.blazor.tests.IntegrationTests
{
    public class ProjectSummaryViewModelTests
    {
        [Fact]
        [Trait("Category","Integration")]
        public async Task SearchProjects_ValidOrganization_GetsProjectsAndSelectsFirstOne()
        {
            // Given
            var root = TestCompositionRoot.CreateIntegration();
            
            var viewModel = root.Get<ProjectSummaryViewModel>();
            await viewModel.Initialize();

            viewModel.Organization = "jrolstad";

            // When
            await viewModel.SearchProjects();

            // Then
            Assert.Null(viewModel.Error);
            Assert.NotEmpty(viewModel.Projects);
            Assert.Equal(viewModel.Projects.First(), viewModel.Project);
        }

        [Fact]
        [Trait("Category", "Integration")]
        public async Task Search_ValidOrganizationAndProject_ShowsResults()
        {
            // Given
            var root = TestCompositionRoot.CreateIntegration();
            
            var viewModel = root.Get<ProjectSummaryViewModel>();
            await viewModel.Initialize();

            viewModel.Organization = "microsoftit";
            viewModel.Project = "oneitvso";
            viewModel.RepositoryFilter = "fin-ffs-tp";
            viewModel.StartDate = DateTime.Today.AddYears(-2);
            // When
            await viewModel.Search();

            // Then
            Assert.Null(viewModel.Error);
            Assert.NotEmpty(viewModel.Results);

            foreach (var result in viewModel.Results)
            {
                Assert.NotNull(result.Repository.Name);
                Assert.NotNull(result.Repository.Url);
            }
        }
    }
}