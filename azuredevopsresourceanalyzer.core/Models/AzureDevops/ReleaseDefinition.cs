using System;
using System.Collections.Generic;

namespace azuredevopsresourceanalyzer.core.Models.AzureDevops
{
    public class ReleaseDefinition
    {
        public string BuildId { get; set; }
        public string RepositoryId { get; set; }
        public string id { get; set; }
        public string name { get; set; }
        public Dictionary<string, Link> _links { get; set; }
    }

    public class Release
    {
        public Dictionary<string, Link> _links { get; set; }
        public string id { get; set; }
        public string name { get; set; }
        public List<ReleaseEnvironment> environments { get; set; }
        public string ReleaseDefinitionId { get; set; }
    }

    public class ReleaseEnvironment
    {
        public string id { get; set; }
        public string name { get; set; }
        public string status { get; set; }
        public List<DeployAttempt> deploySteps { get; set; }

    }

    public class DeployAttempt
    {
        public string id { get; set; }
        public string status { get; set; }
        public DateTime? queuedOn { get; set; }
    }
}