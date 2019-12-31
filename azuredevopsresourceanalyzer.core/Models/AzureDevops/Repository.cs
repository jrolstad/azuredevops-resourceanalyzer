using System.Collections.Generic;

namespace azuredevopsresourceanalyzer.core.Models.AzureDevops
{
    public class Repository
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Url { get; set; }
        public List<CommitSummary> CommitSummary { get; set; }

    }
}