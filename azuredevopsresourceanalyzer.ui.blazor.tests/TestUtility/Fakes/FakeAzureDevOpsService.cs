using System;
using System.Collections.Generic;
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
            return _context.Repositories;
        }

        public async Task<List<BuildDefinition>> GetBuildDefinitions(string organization, string project, string repositoryId)
        {
            return new List<BuildDefinition>();
        }

        public async Task<List<ReleaseDefinition>> GetReleaseDefinitionsByBuild(string organization, string project, string projectId, string buildId)
        {
            return new List<ReleaseDefinition>();
        }

        public async Task<List<ReleaseDefinition>> GetReleaseDefinitionsByRepository(string organization, string project, string projectId, string repositoryId)
        {
            return new List<ReleaseDefinition>();
        }

        public async Task<List<Release>> GetReleases(string organization, string project, string releaseDefinitionId)
        {
            return new List<Release>();
        }

        public async Task<List<GitPullRequest>> GetPullRequests(string organization, string project, string repositoryId)
        {
            return new List<GitPullRequest>();
        }

        public async Task<List<GitBranchStat>> GetBranchStatistics(string organization, string project, string repositoryId)
        {
            return new List<GitBranchStat>();
        }

        public async Task<List<GitCommitRef>> GetRepositoryCommits(string organization, string project, string repositoryId, DateTime? startDate)
        {
           return new List<GitCommitRef>();
        }

        public async Task<List<Project>> GetProjects(string organization)
        {
            return _context.Projects;
        }
    }
}