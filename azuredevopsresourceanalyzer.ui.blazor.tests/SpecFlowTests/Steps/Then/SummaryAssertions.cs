using System;
using System.Linq;
using azuredevopsresourceanalyzer.ui.blazor.tests.SpecFlowTests.Steps.Extensions;
using azuredevopsresourceanalyzer.ui.blazor.tests.TestUtility.Extensions;
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

        [Then(@"Organization is empty")]
        public void ThenOrganizationIsEmpty()
        {
            switch (_context.ViewType())
            {
                case ViewType.ProjectSummary:
                {
                    Assert.Null(_context.ProjectSummary().Organization);
                    break;
                }
                case ViewType.WorkSummary:
                {
                    Assert.Null(_context.WorkSummary().Organization);
                        break;
                }
            }
        }

        [Then(@"Project is empty")]
        public void ThenProjectIsEmpty()
        {
            switch (_context.ViewType())
            {
                case ViewType.ProjectSummary:
                {
                    Assert.Null(_context.ProjectSummary().Project);
                    break;
                }
                case ViewType.WorkSummary:
                {
                    Assert.Null(_context.WorkSummary().Project);
                    break;
                }
            }
        }

        [Then(@"the list of available projects is empty")]
        public void ThenTheListOfAvailableProjectsIsEmpty()
        {
            switch (_context.ViewType())
            {
                case ViewType.ProjectSummary:
                {
                    Assert.Empty(_context.ProjectSummary().Projects);
                    break;
                }
                case ViewType.WorkSummary:
                {
                    Assert.Empty(_context.WorkSummary().Projects);
                    break;
                }
            }
        }

        [Then(@"StartDate is '(.*)'")]
        public void ThenStartDateIs(string startDate)
        {
            var expected = startDate == "[2 Years Ago]" ? 
                DateTime.Today.AddYears(-2) : 
                startDate.ToDateTime();

            switch (_context.ViewType())
            {
                case ViewType.ProjectSummary:
                {
                    Assert.Equal(expected,_context.ProjectSummary().StartDate);

                    break;
                }
                case ViewType.WorkSummary:
                {
                    Assert.Equal(expected,_context.WorkSummary().StartDate);
                    break;
                }
            }
        }

        [Then(@"the page shows as not working")]
        public void ThenThePageShowsAsNotWorking()
        {
            switch (_context.ViewType())
            {
                case ViewType.ProjectSummary:
                {
                    Assert.False(_context.ProjectSummary().IsSearchingProjects);
                    Assert.False(_context.ProjectSummary().IsSearching);
                    break;
                }
                case ViewType.WorkSummary:
                {
                    Assert.False(_context.WorkSummary().IsSearchingProjects);
                    Assert.False(_context.WorkSummary().IsSearching);
                        break;
                }
            }
        }


    }
}