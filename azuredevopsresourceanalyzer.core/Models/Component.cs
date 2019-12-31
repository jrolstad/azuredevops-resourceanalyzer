using System.Collections.Generic;

namespace azuredevopsresourceanalyzer.core.Models
{
    public class Component
    {
        public Repository Repository { get; set; }
        public List<BuildDefinition> BuildDefinitions { get; set; }
        public List<ReleaseDefinition> ReleaseDefinitions { get; set; }
    }
}