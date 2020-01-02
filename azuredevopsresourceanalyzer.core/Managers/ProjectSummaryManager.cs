using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using azuredevopsresourceanalyzer.core.Extensions;
using azuredevopsresourceanalyzer.core.Models;
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

        private IEnumerable<Models.AzureDevops.Repository> FilterRepositories(IEnumerable<Models.AzureDevops.Repository> repositories, string filter)
        {
            return string.IsNullOrWhiteSpace(filter) ? repositories : 
                repositories.Where(r => r.name.ContainsValue(filter,CultureInfo.CurrentCulture));
        }

        private async Task<Component> ProcessComponent(Models.AzureDevops.Repository repository, string organization, string project, DateTime? startDate)
        {
            var pullRequests = await _azureDevopsService.GetPullRequests(organization, project, repository.id);


            var builds = await _azureDevopsService.GetBuildDefinitions(organization, project, repository.id);
            
            var releaseTasks = builds
                .Select(b =>_azureDevopsService.GetReleaseDefinitions(organization, project, b.project?.id, b.id))
                .ToList();
            var releaseData = await Task.WhenAll(releaseTasks);
            var releases = releaseData.SelectMany(r => r);

            var commits = await _azureDevopsService.GetRepositoryCommits(organization, project, repository.id,startDate);

            var result = new Component
            {
                Repository = Map(repository,commits,pullRequests),
                BuildDefinitions = builds.Select(Map).ToList(),
                ReleaseDefinitions = releases.Select(Map).ToList()

            };

            return result;
        }

        private Repository Map(Models.AzureDevops.Repository toMap, 
            ICollection<Models.AzureDevops.Commit> commits,
            ICollection<Models.AzureDevops.PullRequest> pullRequests)
        {
            var commitSummary = Map(commits)?.ToList();
            var pullRequestSummary = Map(pullRequests)?.ToList();
            return new Repository
            {
                Id = toMap.id,
                Name = toMap.name,
                Url = toMap.weburl,
                CommitSummary = commitSummary,
                PullRequestSummary = pullRequestSummary
            };
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
            return new BuildDefinition
            {
                Id = toMap.id,
                Name = toMap.name,
                Url = toMap.url
            };
        }

        private ReleaseDefinition Map(Models.AzureDevops.ReleaseDefinition toMap)
        {
            return new ReleaseDefinition
            {
                Id = toMap.id,
                Name = toMap.name,
                Url = toMap.url
            };
        }
    }
}