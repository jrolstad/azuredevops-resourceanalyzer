using System.Collections.Generic;

namespace azuredevopsresourceanalyzer.core.Models.AzureDevops
{
    public class BuildDefinitionResult
    {
        public List<BuildDefinition> value { get; set; }
    }

    public class BuildDefinition
    {
        public string id { get; set; }
        public string name { get; set; }
        public string url { get; set; }
        public string weburl { get; set; }
    }
}