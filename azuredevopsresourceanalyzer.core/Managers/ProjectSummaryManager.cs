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
using Release = azuredevopsresourceanalyzer.core.Models.AzureDevops.Release;

namespace azuredevopsresourceanalyzer.core.Managers
{
    public class ProjectSummaryManager
    {
        private readonly IAzureDevopsService _azureDevopsService;

        public ProjectSummaryManager(IAzureDevopsService azureDevopsService)
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
             var buildDefinitions = await _azureDevopsService.GetBuildDefinitions(organization, project, repository.id);
            
            var releaseDefinitionForBuildsTasks = buildDefinitions
                .Select(b => _azureDevopsService.GetReleaseDefinitionsByBuild(organization, project, b.project?.id, b.id))
                .ToList();
            var releaseDefinitionForBuildsData = await Task.WhenAll(releaseDefinitionForBuildsTasks);
            var releaseDefinitionForBuilds = releaseDefinitionForBuildsData.SelectMany(r => r).ToList();

            if (IsEmpty(repository))
            {
                return new Component
                {
                    Repository = Map(repository, buildDefinitions: buildDefinitions, releaseDefinitions: releaseDefinitionForBuilds)
                };
            }

            var commitTask = _azureDevopsService.GetRepositoryCommits(organization, project, repository.id,startDate);
            var pullRequestTask = _azureDevopsService.GetPullRequests(organization, project, repository.id);
            var branchTask = _azureDevopsService.GetBranchStatistics(organization, project, repository.id);

            var releaseDefinitionForRepoTask = _azureDevopsService.GetReleaseDefinitionsByRepository(organization, project, repository.project?.id, repository.id);
            var releaseDefinitions = (await Task.WhenAll(releaseDefinitionForRepoTask)).SelectMany(r => r).ToList();
            
            var releasesTasks = releaseDefinitionForBuilds.Union(releaseDefinitions)
                .Select(r => _azureDevopsService.GetReleases(organization, project, r.id));

            var commits = (await Task.WhenAll(commitTask)).SelectMany(r=>r);
            var pullRequests = (await Task.WhenAll(pullRequestTask)).SelectMany(r=>r);
            var branches = (await Task.WhenAll(branchTask)).SelectMany(r=>r);
            var releases = (await Task.WhenAll(releasesTasks)).SelectMany(r => r);

            return new Component
            {
                Repository = Map(repository,commits,pullRequests,branches, buildDefinitions, releaseDefinitionForBuilds, releaseDefinitions, releases),
            };

        }

        private static bool IsEmpty(GitRepository repository)
        {
            return repository.size == 0;
        }

        private Repository Map(GitRepository toMap,
            IEnumerable<GitCommitRef> commits = null,
            IEnumerable<GitPullRequest> pullRequests = null,
            IEnumerable<GitBranchStat> branches = null,
            IEnumerable<Models.AzureDevops.BuildDefinition> buildDefinitions = null,
            IEnumerable<Models.AzureDevops.ReleaseDefinition> releaseDefinitions = null,
            IEnumerable<Models.AzureDevops.ReleaseDefinition> releaseDefinitionsForRepo = null,
            IEnumerable<Models.AzureDevops.Release> releases = null)
        {
            var commitsRealized = commits.ToList();

            var commitSummary = Map(commitsRealized ?? new List<GitCommitRef>())?.ToList();
            var pullRequestSummary = Map(pullRequests ?? new List<GitPullRequest>())?.ToList();
            var branchSummary = Map(branches ?? new List<GitBranchStat>(),toMap)?.ToList();
            var releaseDefinitionsByBuildId = releaseDefinitions?.ToLookup(r => r.BuildId);
            var releasesByReleaseDefinitionId = (releases ?? new List<Release>()).ToLookup(r => r.ReleaseDefinitionId);
            var buildSummary = buildDefinitions?.Select(b=>Map(b,releaseDefinitionsByBuildId, releasesByReleaseDefinitionId)).ToList();
            var repoReleaseDefinitions = Map(releaseDefinitionsForRepo ?? new List<Models.AzureDevops.ReleaseDefinition>(), releasesByReleaseDefinitionId).ToList();
            
            return new Repository
            {
                Id = toMap.id,
                Name = toMap.name,
                Url = toMap.weburl,
                CommitSummary = commitSummary,
                PullRequestSummary = pullRequestSummary,
                Branches = branchSummary,
                BuildDefinitions = buildSummary,
                ReleaseDefinitions = repoReleaseDefinitions
            };
        }

        private IEnumerable<Branch> Map(IEnumerable<GitBranchStat> toMap, Models.AzureDevops.GitRepository repository)
        {
            return toMap?
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
            //For Git commits, author vs committer here is a good explanation: 
            //https://stackoverflow.com/questions/18750808/difference-between-author-and-committer-in-git

            

            // Email addresses and names can be inconsistent. (ex: jorolsta@microsoft.com and josh.rolstad@microsoft.com can both be there but are the same person)
            var nameByEmail = commits.ToLookup(c => c?.author?.email ?? "unknown")
                .ToDictionary(g => g?.Key ?? "unknown", g => g?.FirstOrDefault()?.author?.name ?? "unknown");

            return commits?
                .GroupBy(c => nameByEmail[c.author.email])
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

        private Models.BuildDefinition Map(Models.AzureDevops.BuildDefinition toMap, 
            ILookup<string,Models.AzureDevops.ReleaseDefinition> releasesByBuildId,
            ILookup<string, Models.AzureDevops.Release> releasesByReleaseDefinitionId)
        {
            var link = GetWebUrl(toMap._links);

            var releasesForBuild = releasesByBuildId[toMap.id];
            var releases = Map(releasesForBuild, releasesByReleaseDefinitionId).ToList();

            return new Models.BuildDefinition
            {
                Id = toMap.id,
                Name = toMap.name,
                Url = link,
                ReleaseDefinitions = releases
            };
        }

        private IEnumerable<Models.ReleaseDefinition> Map(IEnumerable<Models.AzureDevops.ReleaseDefinition> toMap,
            ILookup<string, Models.AzureDevops.Release> releasesByReleaseDefinitionId)
        {
            return toMap?
                .Select(r => new Models.ReleaseDefinition
            {
                Id = r.id,
                Name = r.name,
                Url = GetWebUrl(r._links),
                LastProductionRelease = GetLastProductionRelease(r.id,releasesByReleaseDefinitionId)
            });
        }

        private Models.Release GetLastProductionRelease(string releaseDefinitionId,
            ILookup<string, Release> releasesByReleaseDefinitionId)
        {
            var release = releasesByReleaseDefinitionId[releaseDefinitionId]
                .Select(r=>new{ReleaseDate = ProductionReleaseDate(r),Release=r})
                .Where(r=>r.ReleaseDate.HasValue)
                .OrderByDescending(r=>r.ReleaseDate)
                .Select(r=>Map(r.Release,r.ReleaseDate))
                .FirstOrDefault();

            return release;
        }

        private bool IsProductionEnvironment(Models.AzureDevops.ReleaseEnvironment environment)
        {
            return environment.name.ContainsValue("prod")
                   && !environment.name.Contains("pre")
                && !environment.name.Contains("ppe");
        }

        private bool IsSuccessfulRelease(Models.AzureDevops.ReleaseEnvironment environment)
        {
            return environment.status.ContainsValue("succeeded");
        }

        private DateTime? ProductionReleaseDate(Models.AzureDevops.Release release)
        {
            var successfulProductionReleases = release.environments
                .Where(e => IsProductionEnvironment(e) && IsSuccessfulRelease(e))
                .ToList();
            var releaseDate = successfulProductionReleases
                .SelectMany(r => r.deploySteps)
                .Select(s => s.queuedOn)
                .FirstOrDefault();

            return releaseDate;
        }

        private Models.Release Map(Models.AzureDevops.Release toMap, DateTime? releaseDate)
        {
            return new Models.Release
            {
                Id = toMap.id,
                Name = toMap.name,
                DeployedAt = releaseDate,
                Url = GetWebUrl(toMap._links)
            };
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