using System;
using System.Collections.Generic;
using azuredevopsresourceanalyzer.core.Models.AzureDevops;

namespace azuredevopsresourceanalyzer.core.Services
{
    public class AzureDevopsService
    {
        public List<Repository> GetRepositories(string organization, string project)
        {
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
    }
}