using azuredevopsresourceanalyzer.ui.blazor.tests.SpecFlowTests.Steps.Extensions;
using azuredevopsresourceanalyzer.ui.blazor.tests.TestUtility.Extensions;
using TechTalk.SpecFlow;

namespace azuredevopsresourceanalyzer.ui.blazor.tests.SpecFlowTests.Steps.Given
{
    [Binding]
    public class ProjectSetup
    {
        private readonly ScenarioContext _context;

        public ProjectSetup(ScenarioContext injectedContext)
        {
            _context = injectedContext;
        }

        [Given("project '(.*)' in organization '(.*)'")]
        public void GivenOrganizationAndProject(string project, string organization)
        {
            _context.TestRoot().WithProject(project,organization);
        }
    }
}