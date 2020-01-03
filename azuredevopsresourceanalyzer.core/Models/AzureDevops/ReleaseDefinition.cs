using System.Collections.Generic;

namespace azuredevopsresourceanalyzer.core.Models.AzureDevops
{
    public class ReleaseDefinition
    {
        public string BuildId { get; set; }
        public string RepositoryId { get; set; }
        public string id { get; set; }
        public string name { get; set; }
        public string url { get; set; }
        public Dictionary<string,Link> _links { get; set; }
        
    }
}