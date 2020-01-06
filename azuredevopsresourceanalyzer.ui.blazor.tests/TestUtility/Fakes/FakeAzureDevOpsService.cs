using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using azuredevopsresourceanalyzer.core.Models.AzureDevops;
using azuredevopsresourceanalyzer.core.Services;

namespace azuredevopsresourceanalyzer.ui.blazor.tests.TestUtility.Fakes
{
    public class FakeAzureDevOpsService:IAzureDevopsService
    {
        private readonly TestContext _context;

        public FakeAzureDevOpsService(TestContext context)
        {
            _context = context;
        }
        public async Task<List<GitRepository>> GetRepositories(string organization, string project)
        {
            var key = GetOrganizationKey(organization, project);
            if (!_context.Repositories.ContainsKey(key))
                throw new HttpRequestException("Organization or project not found");

            return _context.Repositories[key];
        }

        public async Task<List<BuildDefinition>> GetBuildDefinitions(string organization, string project, string repositoryId)
        {
            var key = GetOrganizationKey(organization, project);
            if(!_context.BuildDefinitions.ContainsKey(key)) return new List<BuildDefinition>();

            var definitions = _context.BuildDefinitions[key];

            return definitions
                .Where(d => d.RepositoryId == repositoryId)
                .ToList();
        }

        public async Task<List<ReleaseDefinition>> GetReleaseDefinitionsByBuild(string organization, string project, string projectId, string buildId)
        {
            var key = GetOrganizationKey(organization, project);
            if (!_context.ReleaseDefinitions.ContainsKey(key)) return new List<ReleaseDefinition>();

            var definitions = _context.ReleaseDefinitions[key];

            return definitions
                .Where(d => d.BuildId == buildId)
                .ToList();
        }

        public async Task<List<ReleaseDefinition>> GetReleaseDefinitionsByRepository(string organization, string project, string projectId, string repositoryId)
        {
            var key = GetOrganizationKey(organization, project);
            if (!_context.ReleaseDefinitions.ContainsKey(key)) return new List<ReleaseDefinition>();

            var definitions = _context.ReleaseDefinitions[key];

            return definitions
                .Where(d => d.RepositoryId == repositoryId)
                .ToList();
        }

        public async Task<List<Release>> GetReleases(string organization, string project, string releaseDefinitionId)
        {
            var key = GetOrganizationKey(organization, project);
            if (!_context.Releases.ContainsKey(key)) return new List<Release>();

            var definitions = _context.Releases[key];

            return definitions
                .Where(d => d.ReleaseDefinitionId == releaseDefinitionId)
                .ToList();
        }

        public async Task<List<GitPullRequest>> GetPullRequests(string organization, string project, string repositoryId)
        {
            var key = GetOrganizationKey(organization, project);
            if (!_context.PullRequests.ContainsKey(key)) return new List<GitPullRequest>();

            var definitions = _context.PullRequests[key];

            return definitions
                .Where(d => d.RepositoryId == repositoryId)
                .ToList();
        }

        public async Task<List<GitBranchStat>> GetBranchStatistics(string organization, string project, string repositoryId)
        {
            var key = GetOrganizationKey(organization, project);
            if (!_context.BranchStats.ContainsKey(key)) return new List<GitBranchStat>();

            var definitions = _context.BranchStats[key];

            return definitions
                .Where(d => d.RepositoryId == repositoryId)
                .ToList();
        }

        public async Task<List<GitCommitRef>> GetRepositoryCommits(string organization, string project, string repositoryId, DateTime? startDate)
        {
            var key = GetOrganizationKey(organization, project);
            if (!_context.Commits.ContainsKey(key)) return new List<GitCommitRef>();

            var definitions = _context.Commits[key];

            return definitions
                .Where(d => d.RepositoryId == repositoryId)
                .ToList();
        }

        public async Task<List<Project>> GetProjects(string organization)
        {
            if (!_context.Projects.ContainsKey(organization))
                throw new HttpRequestException("Organization not found");

            return _context.Projects[organization];
        }

        public async Task<List<WebApiTeam>> GetTeams(string organization)
        {
            if (!_context.Projects.ContainsKey(organization))
                throw new HttpRequestException("Organization not found");

            return _context.Teams[organization];
        }

        private string GetOrganizationKey(string organization, string project)
        {
            return $"{organization}:{project}";
        }
    }
}