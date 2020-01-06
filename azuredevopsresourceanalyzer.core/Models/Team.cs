using System.Collections.Generic;

namespace azuredevopsresourceanalyzer.core.Models
{
    public class Team
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Url { get; set; }

        public List<TeamWorkItemType> WorkItemTypes { get; set; }
    }

    public class TeamWorkItemType
    {
        public string Type { get; set; }
        public Dictionary<string,int> StateCount { get; set; }
    }
}