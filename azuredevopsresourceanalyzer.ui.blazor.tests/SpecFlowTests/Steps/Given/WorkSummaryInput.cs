using azuredevopsresourceanalyzer.ui.blazor.tests.SpecFlowTests.Steps.Extensions;
using azuredevopsresourceanalyzer.ui.blazor.tests.TestUtility.Extensions;
using TechTalk.SpecFlow;

namespace azuredevopsresourceanalyzer.ui.blazor.tests.SpecFlowTests.Steps.Given
{
    [Binding]
    public class WorkSummaryInput
    {
        private readonly ScenarioContext _context;

        public WorkSummaryInput(ScenarioContext injectedContext)
        {
            _context = injectedContext;
        }

        [Given("I enter '(.*)' into Organization on the WorkSummary page")]
        public void OrganizationInput(string organization)
        {
            var view = _context.WorkSummary();
            view.Organization = organization;
        }

        [Given("I enter '(.*)' into Project on the WorkSummary page")]
        public void ProjectInput(string project)
        {
            var view = _context.WorkSummary();
            view.Project = project;
        }

        [Given("I enter '(.*)' into Teams Filter on the WorkSummary page")]
        public void RepoFilterInput(string filter)
        {
            var view = _context.WorkSummary();
            view.TeamsFilter = filter;
        }

        [Given("I enter '(.*)' into Start Date on the WorkSummary page")]
        public void StartDateInput(string startDate)
        {
            var view = _context.WorkSummary();
            view.StartDate = startDate.ToDateTime();
        }
    }
}