using System;
using System.Collections.Generic;
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
        public List<Repository> GetRepositories(string organization, string project)
        {
            var token = GetBearerToken();

            return new List<Repository>();
        }

        public List<BuildDefinition> GetBuildDefinitions(string organization, string project, string repositoryId)
        {
            return new List<BuildDefinition>();
        }

        public List<ReleaseDefinition> GetReleaseDefinitions(string organization, string project, string buildId)
        {
            return new List<ReleaseDefinition>();
        }

        public List<Commit> GetRepositoryCommits(string organization, string project, string repositoryId, DateTime? startDate)
        {
            return new List<Commit>();
        }

        private string GetBearerToken()
        {
            var azureAdTrustedResource = _configurationService.AzureAdTrustedResource();
            var accessToken = "Bearer " + AzureAdTokenService.GetBearerToken(azureAdTrustedResource);

            return accessToken;

        }
    }
}