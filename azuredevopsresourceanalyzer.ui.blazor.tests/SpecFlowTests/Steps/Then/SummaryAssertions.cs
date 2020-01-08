using System.Linq;
using azuredevopsresourceanalyzer.ui.blazor.tests.SpecFlowTests.Steps.Extensions;
using TechTalk.SpecFlow;
using Xunit;

namespace azuredevopsresourceanalyzer.ui.blazor.tests.SpecFlowTests.Steps.Then
{
    [Binding]
    public class SummaryAssertions
    {
        private readonly ScenarioContext _context;

        public SummaryAssertions(ScenarioContext context)
        {
            _context = context;
        }

        [Then(@"the selected project is '(.*)'")]
        public void ThenTheSelectedProjectIs(string project)
        {
            switch (_context.ViewType())
            {
                case ViewType.ProjectSummary:
                {
                    Assert.Equal(project, _context.ProjectSummary().Project);
                        break;
                }
                case ViewType.WorkSummary:
                {
                    Assert.Equal(project, _context.WorkSummary().Project);
                        break;
                }
            }
            
        }

        [Then(@"the list of projects shown are")]
        public void ThenTheListOfProjectsShownAre(Table table)
        {
            var expected = table.Rows
                .Select(r => r[0].Trim())
                .ToList();

            switch (_context.ViewType())
            {
                case ViewType.ProjectSummary:
                {
                    Assert.Equal(expected, _context.ProjectSummary().Projects);
                    break;
                }
                case ViewType.WorkSummary:
                {
                    Assert.Equal(expected, _context.WorkSummary().Projects);
                    break;
                }
            }
        }

        [Then(@"no errors are shown")]
        public void NoErrorsAreShown()
        {
            switch (_context.ViewType())
            {
                case ViewType.ProjectSummary:
                {
                    Assert.Null(_context.ProjectSummary().Error);
                    break;
                }
                case ViewType.WorkSummary:
                {
                    Assert.Null(_context.WorkSummary().Error);
                    break;
                }
            }
        }

        [Then(@"the error '(.*)' is shown")]
        public void TheErrorTextIsShown(string error)
        {
            switch (_context.ViewType())
            {
                case ViewType.ProjectSummary:
                {
                    Assert.Equal(error, _context.ProjectSummary().Error);
                    break;
                }
                case ViewType.WorkSummary:
                {
                    Assert.Equal(error, _context.WorkSummary().Error);
                    break;
                }
            }
        }

        [Then(@"the error is shown")]
        public void TheErrorIsShown()
        {
            switch (_context.ViewType())
            {
                case ViewType.ProjectSummary:
                {
                    Assert.NotNull(_context.ProjectSummary().Error);
                    break;
                }
                case ViewType.WorkSummary:
                {
                    Assert.NotNull(_context.WorkSummary().Error);
                    break;
                }
            }
        }

    }
}