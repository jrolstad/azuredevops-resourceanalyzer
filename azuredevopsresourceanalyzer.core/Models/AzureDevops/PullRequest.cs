namespace azuredevopsresourceanalyzer.core.Models.AzureDevops
{
    public class PullRequest
    {
        public string closedDate { get; set; }
        public IdentifyRef createdBy { get; set; }
        public string creationDate { get; set; }
        public string status { get; set; }
    }

    public class IdentifyRef
    {
        public string displayName { get; set; }
        public string id { get; set; }
    }
}