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

        [Then(@"the project summary results contains contributors for '(.*)'")]
        public void ThenTheProjectSummaryResultsContainsContributorsFor(string repository, Table table)
        {
            var actual = _context.ProjectSummary()
                .Results
                .Where(r => string.Equals(r.Repository.Name, repository, StringComparison.CurrentCultureIgnoreCase))
                .SelectMany(r => r.Contributors)
                .ToDictionary(r => r.Name);

            var expected = table.Rows
                .Select(r => new
                {
                    name = r[0],
                    commitCount = r[1].ToInt32(),
                    lastActivity = r[2].ToDateTime(),
                    deletionCount = r[3].ToInt32(),
                    editCount = r[4].ToInt32(),
                    additionCount = r[5].ToInt32(),
                })
                .ToList();

            Assert.Equal(expected.Count, actual.Count);

            foreach (var contributor in expected)
            {
                Assert.Contains(contributor.name, actual.Keys);
                var actualValue = actual[contributor.name];

                Assert.Equal(contributor.commitCount,actualValue.ActivityCount);
                Assert.Equal(contributor.lastActivity,actualValue.LastActivity);

                Assert.Equal(contributor.additionCount, actualValue.ActivityDetails.Additions);
                Assert.Equal(contributor.editCount, actualValue.ActivityDetails.Edits);
                Assert.Equal(contributor.deletionCount, actualValue.ActivityDetails.Deletions);

               
            }

        }

        [Then(@"the project summary results contains pull requests for '(.*)'")]
        public void ThenTheProjectSummaryResultsContainsPullRequestsFor(string repository, Table table)
        {
            var actual = _context.ProjectSummary()
                .Results
                .Where(r => string.Equals(r.Repository.Name, repository, StringComparison.CurrentCultureIgnoreCase))
                .SelectMany(r => r.PullRequests)
                .ToDictionary(r => r.Name);

            var expected = table.Rows
                .Select(r => new
                {
                    name = r[0],
                    lastActivity = r[1].ToDateTime(),
                    abandonedCount = r[2].ToInt32(),
                    activeCount = r[3].ToInt32(),
                    completedCount = r[4].ToInt32(),
                })
                .ToList();

            Assert.Equal(expected.Count, actual.Count);

            foreach (var requester in expected)
            {
                Assert.Contains(requester.name, actual.Keys);
                var actualValue = actual[requester.name];

                Assert.Equal(requester.lastActivity, actualValue.LastActivity);

                Assert.Equal(requester.abandonedCount, actualValue.ActivityDetails.Abandoned);
                Assert.Equal(requester.activeCount, actualValue.ActivityDetails.Active);
                Assert.Equal(requester.completedCount, actualValue.ActivityDetails.Complete);


            }
        }

        [Then(@"the project summary results contains build definitions for '(.*)'")]
        public void ThenTheProjectSummaryResultsContainsBuildDefinitionsFor(string repository, Table table)
        {
            var actual = _context.ProjectSummary()
                .Results
                .Where(r => string.Equals(r.Repository.Name, repository, StringComparison.CurrentCultureIgnoreCase))
                .SelectMany(r => r.Builds)
                .ToDictionary(r => r.Name);

            var expected = table.Rows
                .Select(r => new
                {
                    name = r[0]
                })
                .ToList();

            Assert.Equal(expected.Count, actual.Count);

            foreach (var build in expected)
            {
                Assert.Contains(build.name, actual.Keys);
                var actualValue = actual[build.name];

                Assert.Equal(build.name, actualValue.Name);
            }
        }

        [Then(@"the project summary results contains release definitions for '(.*)'")]
        public void ThenTheProjectSummaryResultsContainsReleaseDefinitionsFor(string repository, Table table)
        {
            var actual = _context.ProjectSummary()
                .Results
                .Where(r => string.Equals(r.Repository.Name, repository, StringComparison.CurrentCultureIgnoreCase))
                .SelectMany(r => r.Releases)
                .ToDictionary(r => r.ReleaseDefinition.Name);

            var expected = table.Rows
                .Select(r => new
                {
                    name = r[0],
                    lastProductionRelease = r[1],
                    lastDeployed = r[2].ToDateTime()
                })
                .ToList();

            Assert.Equal(expected.Count, actual.Count);

            foreach (var release in expected)
            {
                Assert.Contains(release.name, actual.Keys);
                var actualValue = actual[release.name];
                
                Assert.Equal(release.name,actualValue.ReleaseDefinition.Name);
                Assert.Equal(release.lastDeployed, actualValue.DeployedAt);
                Assert.Equal(release.lastProductionRelease ?? "", actualValue.LastProductionRelease?.Name ?? "");
            }
        }


    }
}