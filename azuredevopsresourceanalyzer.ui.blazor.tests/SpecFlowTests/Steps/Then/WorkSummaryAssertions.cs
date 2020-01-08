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

        

        [Then(@"the work summary results contain teams")]
        public void ThenTheWorkSummaryResultsContainTeams(Table table)
        {
            ScenarioContext.Current.Pending();
        }

        [Then(@"the work summary results contains work item types for '(.*)'")]
        public void ThenTheWorkSummaryResultsContainsWorkItemTypesFor(string team, Table table)
        {
            ScenarioContext.Current.Pending();
        }

        [Then(@"the work summary results contains lifespan metrics for '(.*)'")]
        public void ThenTheWorkSummaryResultsContainsLifespanMetricsFor(string team, Table table)
        {
            ScenarioContext.Current.Pending();
        }

        [Then(@"the work summary results contains contributors for '(.*)'")]
        public void ThenTheWorkSummaryResultsContainsContributorsFor(string team, Table table)
        {
            ScenarioContext.Current.Pending();
        }

        [Then(@"work summary results contains contributor '(.*)' for '(.*)' with work item counts")]
        public void ThenWorkSummaryResultsContainsContributorForWithWorkItemCounts(string contributor, string team, Table table)
        {
            ScenarioContext.Current.Pending();
        }

    }
}