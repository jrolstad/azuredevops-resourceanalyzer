using System;
using System.Linq;
using azuredevopsresourceanalyzer.ui.blazor.tests.SpecFlowTests.Steps.Extensions;
using azuredevopsresourceanalyzer.ui.blazor.tests.TestUtility.Extensions;
using TechTalk.SpecFlow;
using Xunit;

namespace azuredevopsresourceanalyzer.ui.blazor.tests.SpecFlowTests.Steps.Then
{
    [Binding]
    public class ProjectSummaryAssertions
    {
        private readonly ScenarioContext _context;

        public ProjectSummaryAssertions(ScenarioContext injectedContext)
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

        [Then(@"the selected project is '(.*)'")]
        public void ThenTheSelectedProjectIs(string project)
        {
            Assert.Equal(project,_context.ProjectSummary().Project);
        }

        [Then(@"the list of projects shown are")]
        public void ThenTheListOfProjectsShownAre(Table table)
        {
            var expected = table.Rows
                .Select(r => r[0].Trim())
                .ToList();

            Assert.Equal(expected,_context.ProjectSummary().Projects);
        }

        [Then(@"no errors are shown")]
        public void NoErrorsAreShown()
        {
            Assert.Null(_context.ProjectSummary().Error);
        }

        [Then(@"the project summary results contain repositories")]
        public void ThenTheProjectSummaryResultsContainRepositories(Table table)
        {
            var expected = table.Rows
                .Select(r => r[0].Trim())
                .ToList();

            Assert.Equal(expected,_context.ProjectSummary().Results.Select(r=>r.Repository.Name));
        }

        [Then(@"the project summary results contains branches for '(.*)'")]
        public void ThenTheProjectSummaryResultsContainsBranchesFor(string repository, Table table)
        {
            var actual = _context.ProjectSummary()
                .Results
                .Where(r => string.Equals(r.Repository.Name, repository, StringComparison.CurrentCultureIgnoreCase))
                .SelectMany(r => r.Branches)
                .ToDictionary(r=>r.Name.Name);

            var expected = table.Rows
                .Select(r => new
                {
                    name = r[0],
                    behindCount = r[1].ToInt32(),
                    aheadCount = r[2].ToInt32()

                })
                .ToList();
            
            Assert.Equal(expected.Count,actual.Count);

            foreach (var branch in expected)
            {
                Assert.Contains(branch.name,actual.Keys);
                var actualValue = actual[branch.name];

                Assert.Equal(branch.aheadCount,actualValue.CommitsAhead);
                Assert.Equal(branch.behindCount,actualValue.CommitsBehind);

                Assert.NotNull(actualValue.Name.Url);
            }
        }




    }
}