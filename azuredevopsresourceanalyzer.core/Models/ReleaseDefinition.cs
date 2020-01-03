using System;

namespace azuredevopsresourceanalyzer.core.Models
{
    public class ReleaseDefinition
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Url { get; set; }
        public Release LastProductionRelease { get; set; }
    }

    public class Release
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Url { get; set; }
        public DateTime? DeployedAt { get; set; }
    }
}