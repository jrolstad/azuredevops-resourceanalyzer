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

    }
}