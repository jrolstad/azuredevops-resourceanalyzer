using System.Collections.Generic;

namespace azuredevopsresourceanalyzer.core.Models.AzureDevops
{
    public class GitPullRequest
    {
        public IdentifyRef createdBy { get; set; }
        public string creationDate { get; set; }
        public string status { get; set; }
        public string RepositoryId { get; set; }
    }
}