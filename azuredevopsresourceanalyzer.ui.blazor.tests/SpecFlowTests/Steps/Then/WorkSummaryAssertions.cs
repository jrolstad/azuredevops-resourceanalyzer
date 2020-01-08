using System.Linq;
using azuredevopsresourceanalyzer.ui.blazor.tests.SpecFlowTests.Steps.Extensions;
using azuredevopsresourceanalyzer.ui.blazor.tests.TestUtility.Extensions;
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
            var expected = table.Rows
                .Select(r => r[0])
                .ToList();

            Assert.Equal(expected,_context.WorkSummary().Results.Select(t=>t.Team.Name));
           
        }

        [Then(@"the work summary results contains work item types for '(.*)'")]
        public void ThenTheWorkSummaryResultsContainsWorkItemTypesFor(string team, Table table)
        {
            var expected = table.Rows
                .Select(r => new
                {
                    type = r[0],
                    newCount = r[1].ToInt32(),
                    activeCount = r[2].ToInt32(),
                    resolvedCount = r[3].ToInt32(),
                    completeCount = r[4].ToInt32()
                })
                .ToList();

            var actual = _context.WorkSummary().Results
                .Where(r => r.Team.Name == team)
                .SelectMany(r => r.WorkItemTypeCounts)
                .ToDictionary(r=>r.Type);

            Assert.Equal(expected.Select(t=>t.type).OrderBy(t=>t).ToList(),actual.Keys.Select(r=>r).OrderBy(t => t).ToList());

            foreach (var type in expected)
            {
                var matching = actual[type.type];

                Assert.Equal(type.activeCount,matching.Active);
                Assert.Equal(type.completeCount,matching.Closed);
                Assert.Equal(type.newCount,matching.New);
                Assert.Equal(type.resolvedCount,matching.Resolved);

                Assert.True(matching.Visible);
            }
        }

        [Then(@"the work summary results contains lifespan metrics for '(.*)'")]
        public void ThenTheWorkSummaryResultsContainsLifespanMetricsFor(string team, Table table)
        {
            var expected = table.Rows
                .Select(r => new
                {
                    type = r[0],
                    backlogDays = r[1].ToDouble(),
                    activeDays = r[2].ToDouble(),
                    resolvedDays = r[3].ToDouble(),
                    activeToDoneDays = r[4].ToDouble(),
                    endToEndDays = r[5].ToDouble()
                })
                .ToList();

            var actual = _context.WorkSummary().Results
                .Where(r => r.Team.Name == team)
                .SelectMany(r => r.LifespanMetrics)
                .ToDictionary(r => r.Type);

            Assert.Equal(expected.Select(t => t.type).OrderBy(t => t).ToList(), actual.Keys.Select(r => r).OrderBy(t => t).ToList());

            foreach (var type in expected)
            {
                var matching = actual[type.type];

                Assert.Equal(type.backlogDays, matching.InceptionToActiveDays);
                Assert.Equal(type.activeDays, matching.ActiveToResolvedDays);
                Assert.Equal(type.resolvedDays, matching.ResolvedToDoneDays);
                Assert.Equal(type.activeToDoneDays, matching.ActiveToDoneDays);
                Assert.Equal(type.endToEndDays, matching.TotalEndToEndDays);

                Assert.True(matching.Visible);
            }
        }

        [Then(@"the work summary results contains contributors for '(.*)'")]
        public void ThenTheWorkSummaryResultsContainsContributorsFor(string team, Table table)
        {
            //_context.Pending();
        }

        [Then(@"work summary results contains contributor '(.*)' for '(.*)' with work item counts")]
        public void ThenWorkSummaryResultsContainsContributorForWithWorkItemCounts(string contributor, string team, Table table)
        {
            //_context.Pending();
        }

    }
}