using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using System.Web;
using azuredevopsresourceanalyzer.core.Extensions;
using azuredevopsresourceanalyzer.core.Models.AzureDevops;

namespace azuredevopsresourceanalyzer.core.Services
{
    public interface IAzureDevopsService
    {
        Task<List<GitRepository>> GetRepositories(string organization, string project);
        Task<List<BuildDefinition>> GetBuildDefinitions(string organization, string project, string repositoryId);
        Task<List<ReleaseDefinition>> GetReleaseDefinitionsByBuild(string organization, string project, string projectId, string buildId);
        Task<List<ReleaseDefinition>> GetReleaseDefinitionsByRepository(string organization, string project, string projectId, string repositoryId);
        Task<List<Release>> GetReleases(string organization, string project, string releaseDefinitionId);
        Task<List<GitPullRequest>> GetPullRequests(string organization, string project, string repositoryId);
        Task<List<GitBranchStat>> GetBranchStatistics(string organization, string project, string repositoryId);
        Task<List<GitCommitRef>> GetRepositoryCommits(string organization, string project, string repositoryId,DateTime? startDate);
        Task<List<Project>> GetProjects(string organization);
        Task<List<WebApiTeam>> GetTeams(string organization);
        Task<TeamFieldValues> GetTeamFieldValues(string organization, string project, string team);
    }

    public class AzureDevopsService : IAzureDevopsService
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly ConfigurationService _configurationService;


        public AzureDevopsService(IHttpClientFactory httpClientFactory, ConfigurationService configurationService)
        {
            _httpClientFactory = httpClientFactory;
            _configurationService = configurationService;
        }
        public async Task<List<GitRepository>> GetRepositories(string organization, string project)
        {

            var url = $"https://dev.azure.com/{organization}/{project}/_apis/git/repositories?api-version=5.0";
            var client = await GetClient();
            var result = await client.GetAsJson<ApiResult<GitRepository>>(url);

            return result?.value;
        }

        public async Task<List<BuildDefinition>> GetBuildDefinitions(string organization, string project, string repositoryId)
        {
            var url = $"https://dev.azure.com/{organization}/{project}/_apis/build/definitions?api-version=5.1&repositoryId={repositoryId}&repositoryType=TfsGit";
            var client = await GetClient();
            var result = await client.GetAsJson<ApiResult<BuildDefinition>>(url);
            return result?.value;
        }


        public async Task<List<ReleaseDefinition>> GetReleaseDefinitionsByBuild(string organization, string project, string projectId, string buildId)
        {

            var url = $"https://vsrm.dev.azure.com/{organization}/{project}/_apis/release/definitions?api-version=5.1&artifactType=Build&artifactSourceId={projectId}:{buildId}";

            var client = await GetClient();
            var releaseDefinitionResult = await client.GetAsJson<ApiResult<ReleaseDefinition>>(url);
            releaseDefinitionResult.value.ForEach(v => v.BuildId = buildId);

            return releaseDefinitionResult?.value;
        }

        public async Task<List<ReleaseDefinition>> GetReleaseDefinitionsByRepository(string organization, string project, string projectId, string repositoryId)
        {

            var url = $"https://vsrm.dev.azure.com/{organization}/{project}/_apis/release/definitions?api-version=5.1&artifactType=Git&artifactSourceId={projectId}:{repositoryId}";

            var client = await GetClient();
            var releaseDefinitionResult = await client.GetAsJson<ApiResult<ReleaseDefinition>>(url);
            releaseDefinitionResult.value.ForEach(v => v.RepositoryId = repositoryId);

            return releaseDefinitionResult?.value;
        }
        public async Task<List<Release>> GetReleases(string organization, string project, string releaseDefinitionId)
        {
            var url = $"https://vsrm.dev.azure.com/{organization}/{project}/_apis/release/releases?api-version=5.1&definitionId={releaseDefinitionId}&status=active&$expand=environments";

            var client = await GetClient();

            var releaseDefinitionResult = await client.GetAsJson<ApiResult<Release>>(url);
            releaseDefinitionResult.value?.ForEach(r => r.ReleaseDefinitionId = releaseDefinitionId);
            return releaseDefinitionResult?.value;
        }


        public async Task<List<GitPullRequest>> GetPullRequests(string organization, string project, string repositoryId)
        {

            var url = $"https://dev.azure.com/{organization}/{project}/_apis/git/repositories/{repositoryId}/pullrequests?api-version=5.1&searchCriteria.status=all";

            var client = await GetClient();
            var releaseDefinitionResult = await client.GetAsJson<ApiResult<GitPullRequest>>(url);

            return releaseDefinitionResult?.value;
        }

        public async Task<List<GitBranchStat>> GetBranchStatistics(string organization, string project, string repositoryId)
        {
            var url = $"https://dev.azure.com/{organization}/{project}/_apis/git/repositories/{repositoryId}/stats/branches?api-version=5.1";

            var client = await GetClient();

            var releaseDefinitionResult = await client.GetAsJson<ApiResult<GitBranchStat>>(url);

            return releaseDefinitionResult?.value;
        }

        public async Task<List<GitCommitRef>> GetRepositoryCommits(string organization, string project, string repositoryId,DateTime? startDate)
        {
            var url = $"https://dev.azure.com/{organization}/{project}/_apis/git/repositories/{repositoryId}/commits?api-version=5.1&$top=1000&searchCriteria.itemVersion.version=master";

            if (startDate.HasValue)
            {
                url += $"&searchCriteria.fromDate={startDate?.ToString("MM/dd/yyyy")}";
            }
            var client = await GetClient();

            var releaseDefinitionResult = await client.GetAsJson<ApiResult<GitCommitRef>>(url);

            return releaseDefinitionResult?.value;
        }

        public async Task<List<Project>> GetProjects(string organization)
        {
            var url = $"https://dev.azure.com/{organization}/_apis/projects?api-version=5.1";

            var client = await GetClient();
            var releaseDefinitionResult = await client.GetAsJson<ApiResult<Project>>(url);

            return releaseDefinitionResult?.value;
        }

        public async Task<List<WebApiTeam>> GetTeams(string organization)
        {
          

            var client = await GetClient();

            ApiResult<WebApiTeam> result;
            var allData = new List<WebApiTeam>();
            const int batchsize = 1000;
            var skip = 0;
            do
            {
                var url = $"https://dev.azure.com/{organization}/_apis/teams?api-version=5.1-preview.3&$top={batchsize}&$skip={skip}";
                result = await client.GetAsJson<ApiResult<WebApiTeam>>(url);

                allData.AddRange(result.value);

                skip += batchsize;
            } while (result.value.Any());


            return allData;
        }

        public async Task<TeamFieldValues> GetTeamFieldValues(string organization, string project, string team)
        {

            var url = $"https://dev.azure.com/{organization}/{project}/{team}/_apis/work/teamsettings/teamfieldvalues?api-version=5.1";

            var client = await GetClient();
            var result = await client.GetAsJson<TeamFieldValues>(url);

            if (result != null)
            {
                result.Team = team;
            }

            return result;
        }


        private async Task<HttpClient> GetClient()
        {
            var client = _httpClientFactory.CreateClient();
            client.DefaultRequestHeaders.Authorization = await GetAuthenticationHeader();

            return client;
        }
        private string _accessToken = null;
        private async Task<AuthenticationHeaderValue> GetAuthenticationHeader()
        {
            if (string.IsNullOrWhiteSpace(_accessToken))
            {

                var azureAdTrustedResource = _configurationService.AzureAdTrustedResource();
                _accessToken = await AzureAdTokenService.GetBearerToken(azureAdTrustedResource);
            }

            return new AuthenticationHeaderValue("Bearer", _accessToken);
        }
        
    }
}