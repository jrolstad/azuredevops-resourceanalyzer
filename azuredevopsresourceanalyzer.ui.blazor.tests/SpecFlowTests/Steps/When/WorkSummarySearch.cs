using System.Threading.Tasks;
using azuredevopsresourceanalyzer.ui.blazor.tests.SpecFlowTests.Steps.Extensions;
using TechTalk.SpecFlow;

namespace azuredevopsresourceanalyzer.ui.blazor.tests.SpecFlowTests.Steps.When
{
    [Binding]
    public class WorkSummarySearch
    {
        private readonly ScenarioContext _context;

        public WorkSummarySearch(ScenarioContext injectedContext)
        {
            _context = injectedContext;
        }

        [When("I press the Search button on the WorkSummary page")]
        public async Task WhenIPressSearch()
        {
            await _context.WorkSummary().Search();
        }

        [When("I press the Project Search button on the WorkSummary page")]
        public async Task WhenIPressProjectSearch()
        {
            await _context.WorkSummary().SearchProjects();
        }


    }
}