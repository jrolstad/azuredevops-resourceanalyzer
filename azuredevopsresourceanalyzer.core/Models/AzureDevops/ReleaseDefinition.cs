using System.Collections.Generic;

namespace azuredevopsresourceanalyzer.core.Models.AzureDevops
{
    public class ReleaseDefinitionResult
    {
        public List<ReleaseDefinition> value { get; set; }
    }

    public class ReleaseDefinition
    {
        public string id { get; set; }
        public string name { get; set; }
        public string url { get; set; }
    }
}