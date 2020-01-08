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

            return _context.Teams[organization]
                .Select(t=>new WebApiTeam
                {
                    name = t.Name,
                    id = t.Id
                })
                .ToList();
        }

        public async Task<TeamFieldValues> GetTeamFieldValues(string organization, string project, string team)
        {
            var key = GetOrganizationKey(organization, project);
            if (!_context.Teams.ContainsKey(key)) return new TeamFieldValues();

            var values = _context.Teams[key];

            var teamData = values
                .FirstOrDefault(d => d.Name == team);

            var result = new TeamFieldValues
            {
                Team = team,
                values = teamData?.AreaPaths?
                    .Select(a => new TeamFieldValue
                    {
                        value = a.Name,
                        includeChildren = a.IncludeChildren
                    })
                    .ToList()
            };
            

            return result;
        }

        public async Task<List<WorkItemReference>> GetWorkItems(string organization, string project, string team, List<Tuple<string, bool>> areaPaths)
        {
            var key = GetOrganizationKey(organization, project);
            if (!_context.WorkItems.ContainsKey(key)) return new List<WorkItemReference>();

            var values = _context.WorkItems[key];

            return values
                .Where(d => areaPaths.Select(a=>a.Item1).Contains(d.AreaPath))
                .Select(d=>new WorkItemReference
                {
                    id = d.Id
                })
                .ToList();
        }

        public async Task<List<WorkItem>> GetWorkItems(string organization, string project, List<string> workItemIds)
        {
            var key = GetOrganizationKey(organization, project);
            if (!_context.WorkItems.ContainsKey(key)) return new List<WorkItem>();

            var values = _context.WorkItems[key];

            return values
                .Where(d => workItemIds.Contains(d.Id))
                .Select(d => new WorkItem
                {
                    id = d.Id,
                    fields = new Dictionary<string, object>
                    {
                        {"Microsoft.VSTS.Common.ActivatedDate",d.ActivatedAt },
                        {"System.AssignedTo",new Newtonsoft.Json.Linq.JObject{ { "displayName", d.AssignedTo } } },
                        {"Microsoft.VSTS.Common.ClosedDate",d.ClosedAt },
                        {"System.CreatedDate",d.CreatedAt },
                        {"Microsoft.VSTS.Common.ResolvedDate",d.ResolvedAt },
                        {"System.State",d.State },
                        {"System.ChangedDate",d.UpdatedAt },
                        {"System.WorkItemType",d.WorkItemType }
                    }
                })
                .ToList();
        }

        private string GetOrganizationKey(string organization, string project)
        {
            return $"{organization}:{project}";
        }
    }
}