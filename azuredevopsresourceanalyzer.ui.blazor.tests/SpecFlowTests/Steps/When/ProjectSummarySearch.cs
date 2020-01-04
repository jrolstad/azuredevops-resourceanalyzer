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
    public class ProjectSummarySearch
    {
        private readonly ScenarioContext _context;

        public ProjectSummarySearch(ScenarioContext injectedContext)
        {
            _context = injectedContext;
        }

        [When("I press the Search button")]
        public async Task WhenIPressSearch()
        {
            await _context.ProjectSummary().Search();
        }

        [When("I press the Project Search button")]
        public async Task WhenIPressProjectSearch()
        {
            await _context.ProjectSummary().SearchProjects();
        }


    }
}
