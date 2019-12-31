using System;
using System.Collections.Generic;
using System.Linq;
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

        public ProjectSummary GetSummary(string organization,
            string project,
            string repositoryFilter = null,
            DateTime? startDate = null)
        {
            var repositories = _azureDevopsService.GetRepositories(organization, project);
            var filteredRepositories = FilterRepositories(repositories, repositoryFilter);

            var components = filteredRepositories
                .AsParallel()
                .Select(r=>ProcessComponent(r,organization,project,startDate))
                .ToList();

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
                repositories.Where(r => r.Name.Contains(filter));
        }

        private Component ProcessComponent(Models.AzureDevops.Repository repository, string organization, string project, DateTime? startDate)
        {
            var builds = _azureDevopsService.GetBuildDefinitions(organization, project, repository.Id);
            var releases = builds
                .AsParallel()
                .SelectMany(b => _azureDevopsService.GetReleaseDefinitions(organization, project, b.Id))
                .ToList();
            var commits = _azureDevopsService.GetRepositoryCommits(organization, project, repository.Id, startDate);

            var result = new Component
            {
                Repository = Map(repository,commits),
                BuildDefinitions = builds.Select(Map).ToList(),
                ReleaseDefinitions = releases.Select(Map).ToList()

            };

            return result;
        }

        private Repository Map(Models.AzureDevops.Repository toMap, IEnumerable<Models.AzureDevops.Commit> commits)
        {
            var commitSummary = Map(commits).ToList();

            return new Repository
            {
                Id = toMap.Id,
                Name = toMap.Name,
                Url = toMap.Url,
                CommitSummary = commitSummary
            };
        }

        private static IEnumerable<CommitSummary> Map(IEnumerable<Models.AzureDevops.Commit> commits)
        {
            return commits
                .GroupBy(c => c.CommiterName)
                .Select(g => new CommitSummary
                {
                    CommitterName = g.Key,
                    NumberOfCommits = g.Count(),
                    LastCommit = g.Max(c=>c.CommittedAt)
                })
                .OrderByDescending(c=>c.LastCommit);
        }

        private BuildDefinition Map(Models.AzureDevops.BuildDefinition toMap)
        {
            return new BuildDefinition
            {
                Id = toMap.Id,
                Name = toMap.Name,
                Url = toMap.Url
            };
        }

        private ReleaseDefinition Map(Models.AzureDevops.ReleaseDefinition toMap)
        {
            return new ReleaseDefinition
            {
                Id = toMap.Id,
                Name = toMap.Name,
                Url = toMap.Url
            };
        }
    }
}