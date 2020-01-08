namespace azuredevopsresourceanalyzer.core.Models.AzureDevops
{
    public static class WorkItemFieldNames
    {
        public const string Id = "System.Id";
        public const string WorkItemType = "System.WorkItemType";
        public const string State = "System.State";
        public const string CreatedDate = "System.CreatedDate";
        public const string ActivatedDate = "Microsoft.VSTS.Common.ActivatedDate";
        public const string ResolvedDate = "Microsoft.VSTS.Common.ResolvedDate";
        public const string ClosedDate = "Microsoft.VSTS.Common.ClosedDate";
        public const string ChangedDate = "System.ChangedDate";
        public const string AssignedTo = "System.AssignedTo";
        public const string StoryPoints = "Microsoft.VSTS.Scheduling.StoryPoints";
    }

    public static class WorkItemStates
    {
        public const string Removed = "Removed";
        public const string Closed = "Closed";
    }
}