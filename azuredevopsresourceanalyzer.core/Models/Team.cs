using System.Collections.Generic;

namespace azuredevopsresourceanalyzer.core.Models
{
    public class Team
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Url { get; set; }
        public List<string> AreaPaths { get; set; }
    }
}