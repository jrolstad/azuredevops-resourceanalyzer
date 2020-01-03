using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using azuredevopsresourceanalyzer.core.Extensions;
using azuredevopsresourceanalyzer.core.Models;
using azuredevopsresourceanalyzer.core.Models.AzureDevops;
using azuredevopsresourceanalyzer.core.Services;

namespace azuredevopsresourceanalyzer.core.Managers
{
    public class ProjectSummaryManager
    {
        private readonly AzureDevopsService _azureDevopsService;

        public ProjectSummaryManager(AzureDevopsService azureDevopsService)
        {
            _azureDevopsService = azureDevopsService;
        }

        public async Task<ProjectSummary> GetSummary(string organization,
            string project,
            string repositoryFilter = null,
            DateTime? startDate = null)
        {
            var repositories = await _azureDevopsService.GetRepositories(organization, project);
            var filteredRepositories = FilterRepositories(repositories, repositoryFilter);

            var componentTasks = filteredRepositories
                .Select(r=>ProcessComponent(r,organization,project,startDate))
                .ToList();
            var components = await Task.WhenAll(componentTasks);

            var result = new ProjectSummary
            {
                Organization = organization,
                Project = project,
                RepositoryFilter = repositoryFilter,
                StartDate = startDate,
                Components = components
            };

            return result;
        }

        private IEnumerable<GitRepository> FilterRepositories(IEnumerable<Models.AzureDevops.GitRepository> repositories, string filter)
        {
            return string.IsNullOrWhiteSpace(filter) ? repositories : 
                repositories.Where(r => r.name.ContainsValue(filter,CultureInfo.CurrentCulture));
        }

        private async Task<Component> ProcessComponent(GitRepository repository, string organization, string project, DateTime? startDate)
        {
             var builds = await _azureDevopsService.GetBuildDefinitions(organization, project, repository.id);
            
            var releaseTasks = builds
                .Select(b => _azureDevopsService.GetReleaseDefinitions(organization, project, b.project?.id, b.id))
                .ToList();
            var releaseData = await Task.WhenAll(releaseTasks);
            var releases = releaseData.SelectMany(r => r);

            var commitTask = _azureDevopsService.GetRepositoryCommits(organization, project, repository.id,startDate);
            var pullRequestTask = _azureDevopsService.GetPullRequests(organization, project, repository.id);
            var branchTask = _azureDevopsService.GetBranchStatistics(organization, project, repository.id);

            var commits = (await Task.WhenAll(commitTask)).SelectMany(r=>r);
            var pullRequests = (await Task.WhenAll(pullRequestTask)).SelectMany(r=>r);
            var branches = (await Task.WhenAll(branchTask)).SelectMany(r=>r);
            
            var result = new Component
            {
                Repository = Map(repository,commits,pullRequests,branches, builds, releases),
            };

            return result;
        }

        private Repository Map(GitRepository toMap,
            IEnumerable<GitCommitRef> commits,
            IEnumerable<GitPullRequest> pullRequests,
            IEnumerable<GitBranchStat> branches,
            IEnumerable<Models.AzureDevops.BuildDefinition> builds,
            IEnumerable<Models.AzureDevops.ReleaseDefinition> releases)
        {
            var commitSummary = Map(commits)?.ToList();
            var pullRequestSummary = Map(pullRequests)?.ToList();
            var branchSummary = Map(branches,toMap)?.ToList();
            var releasesByBuildId = releases.ToLookup(r => r.BuildId);
            var buildSummary = builds.Select(b=>Map(b,releasesByBuildId)).ToList();

            return new Repository
            {
                Id = toMap.id,
                Name = toMap.name,
                Url = toMap.weburl,
                CommitSummary = commitSummary,
                PullRequestSummary = pullRequestSummary,
                Branches = branchSummary,
                BuildDefinitions = buildSummary
            };
        }

        private IEnumerable<Branch> Map(IEnumerable<GitBranchStat> toMap, Models.AzureDevops.GitRepository repository)
        {
            return toMap
                .Select(m => new Branch
                {
                    Name = m.name,
                    CommitsAhead = m.aheadCount,
                    CommitsBehind = m.behindCount,
                    Url = $"{repository.weburl.TrimEnd('?')}?version=GB{HttpUtility.UrlEncode(m.name)}"
                });
        }

        private static IEnumerable<ContributorSummary> Map(IEnumerable<Models.AzureDevops.GitCommitRef> commits)
        {
            return commits?
                .GroupBy(c => c?.author?.name)
                .Select(g => new ContributorSummary
                {
                    AuthorName = g.Key,
                    CommitCount = g.Count(),
                    LastActivity = g.Max(c=>c?.author?.date?.Date),
                    Additions = g.Sum(c=>c.changeCounts?.Add),
                    Edits = g.Sum(c=>c.changeCounts?.Edit),
                    Deletions = g.Sum(c=>c.changeCounts?.Delete),
                })
                .OrderByDescending(c=>c.LastActivity);
        }

        private static IEnumerable<PullRequestSummary> Map(IEnumerable<Models.AzureDevops.GitPullRequest> pullRequests)
        {
            return pullRequests?
                .GroupBy(c => c?.createdBy?.displayName)
                .Select(g => new PullRequestSummary
                {
                    AuthorName = g.Key,
                    TotalCount = g.Count(),
                    LastActivity = g.Max(c => DateTime.Parse(c.creationDate)),
                    AbandonedCount = g.Count(c=>string.Equals("abandoned",c.status,StringComparison.CurrentCultureIgnoreCase)),
                    ActiveCount = g.Count(c=>string.Equals("active", c.status,StringComparison.CurrentCultureIgnoreCase)),
                    CompletedCount = g.Count(c=>string.Equals("completed", c.status,StringComparison.CurrentCultureIgnoreCase)),
                })
                .OrderByDescending(c => c.LastActivity);
        }

        private Models.BuildDefinition Map(Models.AzureDevops.BuildDefinition toMap, ILookup<string,Models.AzureDevops.ReleaseDefinition> releasesByBuildId)
        {
            var link = GetWebUrl(toMap._links);

            var releasesForBuild = releasesByBuildId[toMap.id];
            var releases = Map(releasesForBuild).ToList();

            return new Models.BuildDefinition
            {
                Id = toMap.id,
                Name = toMap.name,
                Url = link,
                ReleaseDefinitions = releases
            };
        }

        private IEnumerable<Models.ReleaseDefinition> Map(IEnumerable<Models.AzureDevops.ReleaseDefinition> toMap)
        {
            return toMap.Select(r => new Models.ReleaseDefinition
            {
                Id = r.id,
                Name = r.name,
                Url = GetWebUrl(r._links)
            });
        }

        private string GetWebUrl(Dictionary<string, Link> links)
        {
            return links?
                .Where(l => string.Equals(l.Key, "web", StringComparison.InvariantCultureIgnoreCase))
                .Select(l => l.Value?.href)
                .FirstOrDefault();
        }
    }
}