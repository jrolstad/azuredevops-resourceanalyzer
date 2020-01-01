using System.Collections.Generic;

namespace azuredevopsresourceanalyzer.core.Models.AzureDevops
{
    public class RepositoryResult
    {
        public List<Repository> value { get; set; }
    }
    public class Repository
    {
        public string id { get; set; }
        public string name { get; set; }
        public string url { get; set; }
        public string weburl { get; set; }

    }
}