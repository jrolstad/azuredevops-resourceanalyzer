﻿using System.Collections.Generic;

namespace azuredevopsresourceanalyzer.core.Models
{
    public class BuildDefinition
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Url { get; set; }

        public List<ReleaseDefinition> ReleaseDefinitions { get; set; }
    }
}