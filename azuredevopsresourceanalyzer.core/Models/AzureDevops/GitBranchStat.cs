namespace azuredevopsresourceanalyzer.core.Models.AzureDevops
{
    public class GitBranchStat
    {
        public string name { get; set; }
        public int aheadCount { get; set; }
        public int behindCount { get; set; }
        public string RepositoryId { get; set; }
    }
}