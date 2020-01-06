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
        public async Task Search_ValidOrganization_GetsTeams()
        {
            // Given
            var root = TestCompositionRoot.CreateIntegration();

            var viewModel = root.Get<WorkSummaryViewModel>();

            viewModel.Organization = "jrolstad";

            // When
            await viewModel.Search();

            // Then
            Assert.Null(viewModel.Error);
            Assert.NotEmpty(viewModel.Results);
        }
    }
}