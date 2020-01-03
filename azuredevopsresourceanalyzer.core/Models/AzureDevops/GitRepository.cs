using System.Collections.Generic;

namespace azuredevopsresourceanalyzer.core.Models.AzureDevops
{
    public class GitRepository
    {
        public string id { get; set; }
        public string name { get; set; }
        public string url { get; set; }
        public string weburl { get; set; }
        public long size { get; set; }

    }
}