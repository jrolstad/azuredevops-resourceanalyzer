using System;
using System.Linq;
using System.Threading.Tasks;
using azuredevopsresourceanalyzer.ui.blazor.Application.ViewModels;
using azuredevopsresourceanalyzer.ui.blazor.tests.TestUtility;
using azuredevopsresourceanalyzer.ui.blazor.tests.TestUtility.Extensions;
using Xunit;

namespace azuredevopsresourceanalyzer.ui.blazor.tests.Application.ViewModels
{
    public class ProjectSummaryViewModelTests
    {
        [Fact]
        public async Task Initialize_NoParameters_SetsStartDate()
        {
            // Given
            var root = TestCompositionRoot.Create();

            var viewModel = root.Get<ProjectSummaryViewModel>();

            // When
            await viewModel.Initialize();

            // Then
            Assert.Equal(DateTime.Today.AddYears(-2),viewModel.StartDate);
        }

        [Fact]
        public async Task Initialize_NoParameters_SetsOrganizationToNull()
        {
            // Given
            var root = TestCompositionRoot.Create();

            var viewModel = root.Get<ProjectSummaryViewModel>();

            // When
            await viewModel.Initialize();

            // Then
            Assert.Null(viewModel.Organization);
        }

        [Fact]
        public async Task Initialize_NoParameters_SetsErrorToNull()
        {
            // Given
            var root = TestCompositionRoot.Create();

            var viewModel = root.Get<ProjectSummaryViewModel>();

            // When
            await viewModel.Initialize();

            // Then
            Assert.Null(viewModel.Error);
        }

        [Fact]
        public async Task Initialize_NoParameters_SetsProjectToNull()
        {
            // Given
            var root = TestCompositionRoot.Create();

            var viewModel = root.Get<ProjectSummaryViewModel>();

            // When
            await viewModel.Initialize();

            // Then
            Assert.Null(viewModel.Project);
        }

        [Fact]
        public async Task Initialize_NoParameters_SetsProjectsToEmpty()
        {
            // Given
            var root = TestCompositionRoot.Create();

            var viewModel = root.Get<ProjectSummaryViewModel>();

            // When
            await viewModel.Initialize();

            // Then
            Assert.Empty(viewModel.Projects);
        }

        [Fact]
        public async Task Initialize_NoParameters_ShowsAsNotWorking()
        {
            // Given
            var root = TestCompositionRoot.Create();

            var viewModel = root.Get<ProjectSummaryViewModel>();

            // When
            await viewModel.Initialize();

            // Then
            Assert.False(viewModel.IsSearching);
            Assert.False(viewModel.IsSearchingProjects);
        }

        [Fact]
        public async Task SearchProjects_NoOrganization_DoesNothing()
        {
            // Given
            var root = TestCompositionRoot.Create();

            var viewModel = root.Get<ProjectSummaryViewModel>();
            await viewModel.Initialize();

            viewModel.Organization = null;

            // When
            await viewModel.SearchProjects();

            // Then
            Assert.Equal("Unable to search projects; please enter an organization first",viewModel.Error);
            Assert.Empty(viewModel.Projects);
            Assert.Null(viewModel.Project);
        }

        [Fact]
        [Trait("Category", "Integration")]
        public async Task SearchProjects_ValidOrganization_GetsProjectsAndSelectsFirstOne()
        {
            // Given
            var root = TestCompositionRoot.Create();
            root.WithProject("my-project");

            var viewModel = root.Get<ProjectSummaryViewModel>();
            await viewModel.Initialize();

            viewModel.Organization = "jrolstad";

            // When
            await viewModel.SearchProjects();

            // Then
            Assert.Null(viewModel.Error);
            Assert.NotEmpty(viewModel.Projects);
            Assert.Equal(viewModel.Projects.First(),viewModel.Project);
        }

        [Fact]
        [Trait("Category", "Integration")]
        public async Task SearchProjects_InvalidOrganization_ShowsErrors()
        {
            // Given
            var root = TestCompositionRoot.Create();

            var viewModel = root.Get<ProjectSummaryViewModel>();
            await viewModel.Initialize();

            viewModel.Organization = "invalid";

            // When
            await viewModel.SearchProjects();

            // Then
            Assert.NotNull(viewModel.Error);
            Assert.Empty(viewModel.Projects);
        }

        [Fact]
        [Trait("Category", "Integration")]
        public async Task Search_ValidOrganizationAndProject_ShowsResults()
        {
            // Given
            var root = TestCompositionRoot.Create();
            root.WithRepository("FIN-FFS-TP-WRT");

            var viewModel = root.Get<ProjectSummaryViewModel>();
            await viewModel.Initialize();

            viewModel.Organization = "microsoftit";
            viewModel.Project = "oneitvso";
            viewModel.RepositoryFilter = "fin-ffs-tp-wrt";
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

        [Fact]
        [Trait("Category", "Integration")]
        public async Task Search_InvalidOrganization_ShowsErrors()
        {
            // Given
            var root = TestCompositionRoot.Create();

            var viewModel = root.Get<ProjectSummaryViewModel>();
            await viewModel.Initialize();

            viewModel.Organization = "invalid";
            viewModel.Project = "Rolstad";

            // When
            await viewModel.Search();

            // Then
            Assert.NotNull(viewModel.Error);
            Assert.Empty(viewModel.Projects);
        }

        [Fact]
        [Trait("Category", "Integration")]
        public async Task Search_InvalidProject_ShowsErrors()
        {
            // Given
            var root = TestCompositionRoot.Create();

            var viewModel = root.Get<ProjectSummaryViewModel>();
            await viewModel.Initialize();

            viewModel.Organization = "jrolstad";
            viewModel.Project = "invalid";

            // When
            await viewModel.Search();

            // Then
            Assert.NotNull(viewModel.Error);
            Assert.Empty(viewModel.Projects);
        }

        [Fact]
        public async Task Search_NoOrganization_DoesNothing()
        {
            // Given
            var root = TestCompositionRoot.Create();

            var viewModel = root.Get<ProjectSummaryViewModel>();
            await viewModel.Initialize();

            viewModel.Organization = null;
            viewModel.Project = "something";

            // When
            await viewModel.Search();

            // Then
            Assert.Equal("Unable to search; please enter an organization and project first", viewModel.Error);
            Assert.Empty(viewModel.Results);
        }

        [Fact]
        public async Task Search_NoProject_DoesNothing()
        {
            // Given
            var root = TestCompositionRoot.Create();

            var viewModel = root.Get<ProjectSummaryViewModel>();
            await viewModel.Initialize();

            viewModel.Organization = "jrolstad";
            viewModel.Project = null;

            // When
            await viewModel.Search();

            // Then
            Assert.Equal("Unable to search; please enter an organization and project first", viewModel.Error);
            Assert.Empty(viewModel.Results);
        }
    }
}