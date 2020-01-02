using System.Collections.Generic;

namespace azuredevopsresourceanalyzer.core.Models.AzureDevops
{
    public class Repository
    {
        public string id { get; set; }
        public string name { get; set; }
        public string url { get; set; }
        public string weburl { get; set; }

    }

    public class GitBranchStat
    {
        public string name { get; set; }
        public int aheadCount { get; set; }
        public int behindCount { get; set; }
    }
}