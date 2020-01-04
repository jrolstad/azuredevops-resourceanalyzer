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

        [Given("I enter '(.*)' into organization")]
        public void SelectedProjectInput(string organization)
        {
            var view = _context.ProjectSummary();
            view.Organization = organization;
        }


        [Given("Project Summary with organization '(.*)', project '(.*)', repository filter '(.*)' and start date '(.*)'")]
        public void GivenProjectSummaryInputs(string organization, string project, string repositoryFilter, string startDate)
        {
            var view = _context.ProjectSummary();
            view.Organization = organization;
            view.Project = project;
            view.RepositoryFilter = repositoryFilter;
            
            if(!string.IsNullOrWhiteSpace(startDate))
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