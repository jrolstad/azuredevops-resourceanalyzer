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
        public double? InceptionToActiveDays { get; set; }
        public double? ActiveToResolvedDays { get; set; }
        public double? ResolvedToDoneDays { get; set; }
        public double? TotalEndToEndDays { get; set; }
        public double? ActiveToDoneDays { get; set; }
        public int? TotalStoryPointsCompleted { get; set; }
        public double? StoryPointsCompleted { get; set; }
    }

    public class TeamWorkItemContributor
    {
        public string Contributor { get; set; }
        public List<TeamWorkItemType> WorkItemTypes { get; set; }
    }
}