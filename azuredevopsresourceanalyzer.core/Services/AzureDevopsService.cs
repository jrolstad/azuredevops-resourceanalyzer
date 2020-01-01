using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using azuredevopsresourceanalyzer.core.Extensions;
using azuredevopsresourceanalyzer.core.Models.AzureDevops;

namespace azuredevopsresourceanalyzer.core.Services
{
    public class AzureDevopsService
    {
        private readonly ConfigurationService _configurationService;

        public AzureDevopsService(ConfigurationService configurationService)
        {
            _configurationService = configurationService;
        }
        public async Task<List<Repository>> GetRepositories(string organization, string project)
        {

            var client = new HttpClient();
            client.DefaultRequestHeaders.Authorization = GetAuthenticationHeader();

            var url = $"https://dev.azure.com/{organization}/{project}/_apis/git/repositories?api-version=5.0";
            var result = await client.GetAsJson<RepositoryResult>(url);

            return result?.value;
        }

        public async Task<List<BuildDefinition>> GetBuildDefinitions(string organization, string project, string repositoryId)
        {
            return new List<BuildDefinition>();
        }

        public async Task<List<ReleaseDefinition>> GetReleaseDefinitions(string organization, string project, string buildId)
        {
            return new List<ReleaseDefinition>();
        }

        public async Task<List<Commit>> GetRepositoryCommits(string organization, string project, string repositoryId, DateTime? startDate)
        {
            return new List<Commit>();
        }

        private AuthenticationHeaderValue GetAuthenticationHeader()
        {
            var azureAdTrustedResource = _configurationService.AzureAdTrustedResource();
            var accessToken =  AzureAdTokenService.GetBearerToken(azureAdTrustedResource);

            return new AuthenticationHeaderValue("Bearer",accessToken);

        }
    }
}