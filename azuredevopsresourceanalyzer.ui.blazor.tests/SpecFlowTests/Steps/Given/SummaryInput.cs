using System;
using azuredevopsresourceanalyzer.ui.blazor.tests.SpecFlowTests.Steps.Extensions;
using azuredevopsresourceanalyzer.ui.blazor.tests.TestUtility.Extensions;
using TechTalk.SpecFlow;

namespace azuredevopsresourceanalyzer.ui.blazor.tests.SpecFlowTests.Steps.Given
{
    [Binding]
    public class SummaryInput
    {
        private readonly ScenarioContext _context;

        public SummaryInput(ScenarioContext injectedContext)
        {
            _context = injectedContext;
        }

        [Given("I enter '(.*)' into Organization")]
        public void OrganizationInput(string organization)
        {
            switch (_context.ViewType())
            {
                case ViewType.ProjectSummary:
                {
                    _context.ProjectSummary().Organization = organization;
                    break;
                }
                case ViewType.WorkSummary:
                {
                    _context.WorkSummary().Organization = organization; 
                    break;
                }
            }
        }

        [Given("I enter '(.*)' into Project")]
        public void ProjectInput(string project)
        {
            switch (_context.ViewType())
            {
                case ViewType.ProjectSummary:
                {
                    _context.ProjectSummary().Project = project;
                    break;
                }
                case ViewType.WorkSummary:
                {
                    _context.WorkSummary().Project = project;
                    break;
                }
            }
        }

        [Given("I enter '(.*)' into Start Date")]
        public void StartDateInput(string startDate)
        {
            switch (_context.ViewType())
            {
                case ViewType.ProjectSummary:
                {
                    _context.ProjectSummary().StartDate = startDate.ToDateTime();
                    break;
                }
                case ViewType.WorkSummary:
                {
                    _context.WorkSummary().StartDate = startDate.ToDateTime();
                    break;
                }
            }
        }

        [Given("I enter '(.*)' into Repository Filter")]
        public void RepoFilterInput(string filter)
        {
            var view = _context.ProjectSummary();
            view.RepositoryFilter = filter;
        }

        [Given("I enter '(.*)' into Team Filter")]
        public void RepoTeamInput(string filter)
        {
            var view = _context.WorkSummary();
            view.TeamsFilter = filter;
        }
    }
}