using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using azuredevopsresourceanalyzer.core.Extensions;
using azuredevopsresourceanalyzer.core.Models;
using azuredevopsresourceanalyzer.core.Models.AzureDevops;
using azuredevopsresourceanalyzer.core.Services;
using BuildDefinition = azuredevopsresourceanalyzer.core.Models.BuildDefinition;
using ReleaseDefinition = azuredevopsresourceanalyzer.core.Models.ReleaseDefinition;
using Repository = azuredevopsresourceanalyzer.core.Models.Repository;

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

        private IEnumerable<Models.AzureDevops.Repository> FilterRepositories(IEnumerable<Models.AzureDevops.Repository> repositories, string filter)
        {
            return string.IsNullOrWhiteSpace(filter) ? repositories : 
                repositories.Where(r => r.name.ContainsValue(filter,CultureInfo.CurrentCulture));
        }

        private async Task<Component> ProcessComponent(Models.AzureDevops.Repository repository, string organization, string project, DateTime? startDate)
        {
             var builds = await _azureDevopsService.GetBuildDefinitions(organization, project, repository.id);
            
            var releaseTasks = builds
                .Select(b =>_azureDevopsService.GetReleaseDefinitions(organization, project, b.project?.id, b.id))
                .ToList();
            var releaseData = await Task.WhenAll(releaseTasks);
            var releases = releaseData.SelectMany(r => r);

            var commitTask = _azureDevopsService.GetRepositoryCommits(organization, project, repository.id,startDate);
            var pullRequestTask = _azureDevopsService.GetPullRequests(organization, project, repository.id);
            var branchTask = _azureDevopsService.GetBranchStatistics(organization, project, repository.id);

            var commits = (await Task.WhenAll(commitTask)).SelectMany(r=>r).ToList();
            var pullRequests = (await Task.WhenAll(pullRequestTask)).SelectMany(r=>r).ToList();
            var branches = (await Task.WhenAll(branchTask)).SelectMany(r=>r).ToList();

            var result = new Component
            {
                Repository = Map(repository,commits,pullRequests,branches),
                BuildDefinitions = builds.Select(Map).ToList(),
                ReleaseDefinitions = releases.Select(Map).ToList()

            };

            return result;
        }

        private Repository Map(Models.AzureDevops.Repository toMap,
            ICollection<Commit> commits,
            ICollection<PullRequest> pullRequests,
            ICollection<GitBranchStat> branches)
        {
            var commitSummary = Map(commits)?.ToList();
            var pullRequestSummary = Map(pullRequests)?.ToList();
            var branchSummary = Map(branches)?.ToList();

            return new Repository
            {
                Id = toMap.id,
                Name = toMap.name,
                Url = toMap.weburl,
                CommitSummary = commitSummary,
                PullRequestSummary = pullRequestSummary,
                Branches = branchSummary
            };
        }

        private IEnumerable<Branch> Map(IEnumerable<GitBranchStat> toMap)
        {
            return toMap
                .Select(m => new Branch
                {
                    Name = m.name,
                    CommitsAhead = m.aheadCount,
                    CommitsBehind = m.behindCount
                });
        }

        private static IEnumerable<ActivitySummary> Map(IEnumerable<Models.AzureDevops.Commit> commits)
        {
            return commits?
                .GroupBy(c => c?.author?.name)
                .Select(g => new ActivitySummary
                {
                    CommitterName = g.Key,
                    Count = g.Count(),
                    LastActivity = g.Max(c=>c?.author?.date?.Date)
                })
                .OrderByDescending(c=>c.LastActivity);
        }

        private static IEnumerable<ActivitySummary> Map(IEnumerable<Models.AzureDevops.PullRequest> pullRequests)
        {
            return pullRequests?
                .GroupBy(c => c?.createdBy?.displayName)
                .Select(g => new ActivitySummary
                {
                    CommitterName = g.Key,
                    Count = g.Count(),
                    LastActivity = g.Max(c => DateTime.Parse(c.creationDate))
                })
                .OrderByDescending(c => c.LastActivity);
        }

        private BuildDefinition Map(Models.AzureDevops.BuildDefinition toMap)
        {
            var link = toMap._links?
                .Where(l => string.Equals(l.Key, "web", StringComparison.InvariantCultureIgnoreCase))
                .Select(l => l.Value?.href)
                .FirstOrDefault();
            return new BuildDefinition
            {
                Id = toMap.id,
                Name = toMap.name,
                Url = link
            };
        }

        private ReleaseDefinition Map(Models.AzureDevops.ReleaseDefinition toMap)
        {
            var link = toMap._links?
                .Where(l=>string.Equals(l.Key,"web",StringComparison.InvariantCultureIgnoreCase))
                .Select(l=>l.Value?.href)
                .FirstOrDefault();
            return new ReleaseDefinition
            {
                Id = toMap.id,
                Name = toMap.name,
                Url = link
            };
        }
    }
}