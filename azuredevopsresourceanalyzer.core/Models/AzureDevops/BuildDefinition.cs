﻿using System.Collections.Generic;

namespace azuredevopsresourceanalyzer.core.Models.AzureDevops
{
    public class BuildDefinition
    {
        public string id { get; set; }
        public string name { get; set; }
        public string url { get; set; }
        public Project project { get; set; }
        public Dictionary<string, Link> _links { get; set; }
        public string RepositoryId { get; set; }
    }
}