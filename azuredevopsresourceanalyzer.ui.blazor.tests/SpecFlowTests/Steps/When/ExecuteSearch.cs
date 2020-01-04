using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using azuredevopsresourceanalyzer.ui.blazor.tests.SpecFlowTests.Steps.Extensions;
using TechTalk.SpecFlow;

namespace azuredevopsresourceanalyzer.ui.blazor.tests.SpecFlowTests.Steps.When
{
    [Binding]
    public sealed class ExecuteSearch
    {
        private readonly ScenarioContext _context;

        public ExecuteSearch(ScenarioContext injectedContext)
        {
            _context = injectedContext;
        }

        [When("I press the Search button")]
        public async Task WhenIPressSearch()
        {
            await _context.ProjectSummary().Search();
        }

        
    }
}
