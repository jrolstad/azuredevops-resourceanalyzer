using System.Threading.Tasks;
using azuredevopsresourceanalyzer.ui.blazor.tests.SpecFlowTests.Steps.Extensions;
using TechTalk.SpecFlow;

namespace azuredevopsresourceanalyzer.ui.blazor.tests.SpecFlowTests.Steps.When
{
    [Binding]
    public sealed class ExecuteSearchProject
    {
        private readonly ScenarioContext _context;

        public ExecuteSearchProject(ScenarioContext injectedContext)
        {
            _context = injectedContext;
        }

        [When("I press the Project Search button")]
        public async Task WhenIPressProjectSearch()
        {
            await _context.ProjectSummary().SearchProjects();
        }

        
    }
}