using System.Collections.Generic;

namespace azuredevopsresourceanalyzer.core.Models.AzureDevops
{
    public class PullRequest
    {
        public string closedDate { get; set; }
        public IdentifyRef createdBy { get; set; }
        public string creationDate { get; set; }
        public string status { get; set; }

        public List<ResourceReference> workItems { get; set; }
    }
}