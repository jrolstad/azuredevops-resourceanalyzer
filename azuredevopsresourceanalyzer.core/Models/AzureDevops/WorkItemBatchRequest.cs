using System.Collections.Generic;

namespace azuredevopsresourceanalyzer.core.Models.AzureDevops
{
    public class WorkItemBatchRequest
    {
        public List<string> fields { get; set; }
        public List<string> ids { get; set; }
    }
}