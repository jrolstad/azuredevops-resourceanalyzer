using System;
using azuredevopsresourceanalyzer.ui.blazor.tests.SpecFlowTests.Steps.Extensions;
using TechTalk.SpecFlow;
using Xunit;

namespace azuredevopsresourceanalyzer.ui.blazor.tests.SpecFlowTests.Steps.Then
{
    [Binding]
    public class ProjectSummaryResults
    {
        private readonly ScenarioContext _context;

        public ProjectSummaryResults(ScenarioContext injectedContext)
        {
            _context = injectedContext;
        }

        [Then("(.*) components are shown")]
        public void ComponentsAreShown(string countInput)
        {
            var count = Int32.Parse(countInput);

            var componentsOnView = _context.ProjectSummary().Results;

            Assert.Equal(count,componentsOnView.Count);
        }
    }
}