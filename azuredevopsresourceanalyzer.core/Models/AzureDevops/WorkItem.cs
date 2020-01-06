using System.Collections.Generic;

namespace azuredevopsresourceanalyzer.core.Models.AzureDevops
{
    public class WorkItem
    {
        public string id { get; set; }
        public Dictionary<string,Link> _links { get; set; }
        public Dictionary<string,object> fields { get; set; }
    }
}