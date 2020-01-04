using azuredevopsresourceanalyzer.ui.blazor.tests.SpecFlowTests.Steps.Extensions;
using azuredevopsresourceanalyzer.ui.blazor.tests.TestUtility.Extensions;
using TechTalk.SpecFlow;

namespace azuredevopsresourceanalyzer.ui.blazor.tests.SpecFlowTests.Steps.Given
{
    [Binding]
    public class ProjectSetup
    {
        private readonly ScenarioContext _context;

        public ProjectSetup(ScenarioContext injectedContext)
        {
            _context = injectedContext;
        }

        [Given("project '(.*)' in organization '(.*)'")]
        public void GivenOrganizationAndProject(string project, string organization)
        {
            _context.TestRoot().WithProject(project,organization:organization);
            _context.Project(project);
            _context.Organization(organization);

        }

        [Given(@"repository '(.*)' with branches")]
        public void GivenRepositoryWithBranches(string repository, Table table)
        {
            var root = _context.TestRoot();
            root.WithRepository(repository,
                project:_context.Project(), 
                organization:_context.Organization());

            foreach (var row in table.Rows)
            {
                var branchName = row[0];
                var behindCount = row[1].ToInt32();
                var aheadCount = row[2].ToInt32();

                root.WithBranch(repository, branchName,
                    aheadCount,
                    behindCount,
                    _context.Organization(),
                    _context.Project());
            }
        }

        [Given(@"commits for '(.*)'")]
        public void GivenRepositoryWithCommits(string repository, Table table)
        {
            var root = _context.TestRoot();

            foreach (var commit in table.Rows)
            {
                root.WithCommit(repository,
                    commit[0],
                    commit[1],
                    commit[2].ToDateTime(),
                    commit[3].ToInt32(),
                    commit[4].ToInt32(),
                    commit[5].ToInt32(),
                    _context.Organization(),
                    _context.Project());
            }

        }

        [Given(@"pull requests for '(.*)'")]
        public void GivenPullRequestsFor(string repository, Table table)
        {
            var root = _context.TestRoot();

            foreach (var item in table.Rows)
            {
                root.WithPullRequest(repository,
                    item[0],
                    item[1].ToDateTime(),
                    item[2],
                    _context.Organization(),
                    _context.Project());
            }
        }
    }
}