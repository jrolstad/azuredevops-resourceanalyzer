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
    public class SummarySearch
    {
        private readonly ScenarioContext _context;

        public SummarySearch(ScenarioContext injectedContext)
        {
            _context = injectedContext;
        }

        [When("I press the Search button")]
        public async Task WhenIPressSearch()
        {
            switch (_context.ViewType())
            {
                case ViewType.ProjectSummary:
                {
                    await _context.ProjectSummary().Search();
                    break;
                }
                case ViewType.WorkSummary:
                {
                    await _context.WorkSummary().Search();
                    break;
                }
            }
        }

        [When("I press the Project Search button")]
        public async Task WhenIPressProjectSearch()
        {
            switch (_context.ViewType())
            {
                case ViewType.ProjectSummary:
                {
                    await _context.ProjectSummary().SearchProjects();
                    break;
                }
                case ViewType.WorkSummary:
                {
                    await _context.WorkSummary().SearchProjects();
                    break;
                }
            }
        }


    }
}
