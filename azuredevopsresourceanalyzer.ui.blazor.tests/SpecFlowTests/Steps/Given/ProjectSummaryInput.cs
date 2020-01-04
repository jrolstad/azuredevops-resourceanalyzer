using System;
using azuredevopsresourceanalyzer.ui.blazor.tests.SpecFlowTests.Steps.Extensions;
using azuredevopsresourceanalyzer.ui.blazor.tests.TestUtility.Extensions;
using TechTalk.SpecFlow;

namespace azuredevopsresourceanalyzer.ui.blazor.tests.SpecFlowTests.Steps.Given
{
    [Binding]
    public class ProjectSummaryInput
    {
        private readonly ScenarioContext _context;

        public ProjectSummaryInput(ScenarioContext injectedContext)
        {
            _context = injectedContext;
        }

        [Given("I enter '(.*)' into Organization")]
        public void OrganizationInput(string organization)
        {
            var view = _context.ProjectSummary();
            view.Organization = organization;
        }

        [Given("I enter '(.*)' into Project")]
        public void ProjectInput(string project)
        {
            var view = _context.ProjectSummary();
            view.Project = project;
        }

        [Given("I enter '(.*)' into Repository Filter")]
        public void RepoFilterInput(string filter)
        {
            var view = _context.ProjectSummary();
            view.RepositoryFilter = filter;
        }
        [Given("I enter '(.*)' into Start Date")]
        public void StartDateInput(string startDate)
        {
            var view = _context.ProjectSummary();
            if (!string.IsNullOrWhiteSpace(startDate))
            {
                view.StartDate = DateTime.Parse(startDate);
            }
            else
            {
                view.StartDate = null;
            }
        }
    }
}