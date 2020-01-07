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
        public List<TeamWorkItemContributor> Contributors { get; set; }
    }

    public class TeamWorkItemType
    {
        public string Type { get; set; }
        public Dictionary<string,int> StateCount { get; set; }
        public TeamWorkItemTypeMetrics Metrics { get; set; }

    }

    public class TeamWorkItemTypeMetrics
    {
        public decimal? InceptionToAgreedUponDays { get; set; }
        public decimal? AgreedUponToActiveDays { get; set; }
        public decimal? ActiveToResolvedDays { get; set; }
        public decimal? ResolvedToDoneDays { get; set; }
        public decimal? AgreedUponToDoneDays { get; set; }
        public decimal? TotalEndToEndDays { get; set; }
    }

    public class TeamWorkItemContributor
    {
        public string Contributor { get; set; }
        public List<TeamWorkItemType> WorkItemTypes { get; set; }
    }
}