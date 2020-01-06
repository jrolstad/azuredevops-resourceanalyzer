using System.Collections.Generic;

namespace azuredevopsresourceanalyzer.core.Models.AzureDevops
{
    public class WorkItemQueryRequest
    {
        public string query { get; set; }
    }

    public class WorkItemQueryResult
    {
        public List<WorkItemReference> workItems { get; set; }
    }
}