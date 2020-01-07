using System.Linq;
using azuredevopsresourceanalyzer.ui.blazor.tests.SpecFlowTests.Steps.Extensions;
using TechTalk.SpecFlow;
using Xunit;

namespace azuredevopsresourceanalyzer.ui.blazor.tests.SpecFlowTests.Steps.Then
{
    [Binding]
    public class WorkSummaryAssertions
    {
        private readonly ScenarioContext _context;

        public WorkSummaryAssertions(ScenarioContext context)
        {
            _context = context;
        }

        [Then(@"the list of projects shown on the WorkSummary page are")]
        public void ThenTheListOfProjectsShownAre(Table table)
        {
            var expected = table.Rows
                .Select(r => r[0].Trim())
                .ToList();

            Assert.Equal(expected, _context.WorkSummary().Projects);
        }

        [Then(@"no errors are shown on the WorkSummary page")]
        public void NoErrorsAreShown()
        {
            Assert.Null(_context.WorkSummary().Error);
        }

        [Then(@"the selected project on the WorkSummary page is '(.*)'")]
        public void ThenTheSelectedProjectIs(string project)
        {
            Assert.Equal(project, _context.WorkSummary().Project);
        }
    }
}