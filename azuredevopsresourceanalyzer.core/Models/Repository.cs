using System;
using System.Collections.Generic;
using azuredevopsresourceanalyzer.core.Models.AzureDevops;

namespace azuredevopsresourceanalyzer.core.Models
{
    public class Repository
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Url { get; set; }
        public List<ContributorSummary> CommitSummary { get; set; }
        public List<PullRequestSummary> PullRequestSummary { get; set; }
        public List<Branch> Branches { get; set; }
        public List<BuildDefinition> BuildDefinitions { get; set; }
        public IEnumerable<ReleaseDefinition> ReleaseDefinitions { get; set; }
    }
}