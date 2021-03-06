﻿using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
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

        [Given(@"build definitions for '(.*)'")]
        public void GivenBuildDefinitionsFor(string repository, Table table)
        {
            var root = _context.TestRoot();

            foreach (var item in table.Rows)
            {
                root.WithBuildDefinition(
                    item[0],
                    repository,
                    organization:_context.Organization(),
                    project:_context.Project());
            }
        }

        [Given(@"release definitions for build '(.*)'")]
        public void GivenReleaseDefinitionsForBuild(string build, Table table)
        {
            var root = _context.TestRoot();

            foreach (var item in table.Rows)
            {
                root.WithReleaseDefinition(
                    item[0],
                    buildName:build,
                    organization: _context.Organization(),
                    project: _context.Project());
            }
        }

        [Given(@"release definitions for repository '(.*)'")]
        public void GivenReleaseDefinitionsForRepository(string repository, Table table)
        {
            var root = _context.TestRoot();

            foreach (var item in table.Rows)
            {
                root.WithReleaseDefinition(
                    item[0],
                    repositoryName: repository,
                    organization: _context.Organization(),
                    project: _context.Project());
            }
        }

        [Given(@"releases for release definition '(.*)'")]
        public void GivenReleasesForReleaseDefinition(string releaseDefinition, Table table)
        {
            var root = _context.TestRoot();

            foreach (var item in table.Rows)
            {
                root.WithRelease(
                    item[0],
                    releaseDefinitionName:releaseDefinition,
                    organization: _context.Organization(),
                    project: _context.Project());

                root.WithReleaseEnvironment(
                    item[1],
                    item[0],
                    item[2],
                    deployedAt:item[3].ToDateTime(),
                    organization: _context.Organization(),
                    project: _context.Project());
            }
        }

        [Given("the (.*) page is loaded")]
        public async Task GivenThePageIsLoaded(string page)
        {
            switch (page)
            {
                case "ProjectSummary":
                {
                    _context.ViewType(ViewType.ProjectSummary);
                    await _context.ProjectSummary().Initialize();
                    break;
                }
                case "WorkSummary":
                {
                    _context.ViewType(ViewType.WorkSummary);
                    await _context.WorkSummary().Initialize();
                        break;
                }
            }
        }

        [Given(@"and teams")]
        public void GivenAndTeams(Table table)
        {
            var teams = table.Rows
                .Select(r => new
                {
                    name=r[0],
                    areaPath = r[1]
                })
                .ToList();

            var root = _context.TestRoot();

            foreach (var team in teams)
            {
                root.WithTeam(team.name,
                    areaPaths: team.areaPath.Split(';'),
                    organization:_context.Organization());
            }
        }

        [Given(@"work items with type '(.*)'")]
        public void GivenWorkItemsWithType(string type, Table table)
        {
            var workItems = table.Rows
                .Select(r => new
                {
                    title = r[0],
                    areaPath = r[1],
                    status = r[2],
                    assignedTo = r[3],
                    updatedAt = r[4].ToDateTime(),
                    createdAt = r[5].ToDateTime(),
                    activatedAt = r[6].ToDateTime(),
                    resolvedAt = r[7].ToDateTime(),
                    closedAt = r[8].ToDateTime()
                })
                .ToList();

            var root = _context.TestRoot();

            foreach (var item in workItems)
            {
                root.WithWorkItem(type,
                    item.title,
                    item.areaPath,
                    item.status,
                    item.assignedTo,
                    item.updatedAt,
                    item.createdAt,
                    item.activatedAt,
                    item.resolvedAt,
                    item.closedAt,
                    _context.Organization(),
                    _context.Project());
            }
        }
    }

    
}