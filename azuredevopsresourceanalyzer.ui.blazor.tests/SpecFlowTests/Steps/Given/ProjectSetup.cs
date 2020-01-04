﻿using azuredevopsresourceanalyzer.ui.blazor.tests.SpecFlowTests.Steps.Extensions;
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

    }
}